using Dapper;
using Microsoft.AspNetCore.Mvc;
using QLKS_115_Nhom3_BE.DTOs;
using QLKS_115_Nhom3_BE.Models;
using System.Data;
using System.Data.Common;

namespace QLKS_115_Nhom3_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhongController : Controller
    {
        private readonly IDbConnection _db;

        public PhongController(IDbConnection db)
        {
            _db = db;
        }
        [HttpPost]
        public async Task<ActionResult<PhongDTO>> Create([FromBody] CreatePhongDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var checkExistPhong = await _db.QueryFirstOrDefaultAsync<CreatePhongDTO>(@"SELECT * FROM Phong WHERE SoPhong = @SoPhong", new { SoPhong = model.SoPhong });
            if (checkExistPhong != null)
            {
               return BadRequest("Số phòng đã tồn tại");
            }
            var parameters = new DynamicParameters();
            parameters.Add("@SoPhong", model.SoPhong);
            parameters.Add("@LoaiPhong", model.MaLoaiPhong);
            parameters.Add("@TinhTrangPhong", model.TinhTrangPhong);
            parameters.Add("@MaPhong", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _db.ExecuteAsync(
                "sp_ThemPhong",
                parameters,
                commandType: CommandType.StoredProcedure
                );
            var phongMoi = await _db.QueryFirstOrDefaultAsync<PhongDTO>(
                    "SELECT SoPhong, TinhTrangPhong, LoaiPhong, SoGiuong, GhiChu FROM Phong AS p JOIN LoaiPhong AS lp on lp.MaLoaiPhong = p.LoaiPhong WHERE p.MaPhong = @Id",
                    new { Id = parameters.Get<int>("@MaPhong") });
            return Ok(phongMoi);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<PhongDTO>>> Get(int page = 1, int pageSize = 10)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Page", page);
            parameters.Add("@PageSize", pageSize);

            using var multi = await _db.QueryMultipleAsync(
                "sp_LayDanhSachPhong",
                param: parameters,
                commandType: CommandType.StoredProcedure
            );

            var data = (await multi.ReadAsync<PhongDTO>()).ToList();
            var totalRecords = await multi.ReadFirstAsync<int>();

            var result = new PagedResult<PhongDTO>
            {
                TotalRecords = totalRecords,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                Data = data
            };

            return Ok(result);
        }


        // GET api/Phong/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PhongDTO>> Get(int id)
        {
            var result = await _db.QueryFirstOrDefaultAsync<PhongDTO>(
                "SELECT MaPhong, SoPhong, TinhTrangPhong, MaLoaiPhong, SoGiuong, GhiChu FROM Phong AS p JOIN LoaiPhong AS lp on lp.MaLoaiPhong = p.LoaiPhong WHERE p.MaPhong = @Id",
                new { Id = id });

            return result == null ? NotFound() : Ok(result);
        }

        // PUT api/Phong/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdatePhongDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var currentRoom = await _db.QueryFirstOrDefaultAsync<UpdatePhongDTO>(
                                       "SELECT * FROM Phong WHERE MaPhong = @Id",
                                                          new { Id = id });
                if (currentRoom == null)
                {
                    return NotFound();
                }

                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                parameters.Add("@SoPhong", string.IsNullOrWhiteSpace(model.SoPhong) ? currentRoom.SoPhong : model.SoPhong);
                parameters.Add("@MaLoaiPhong", model.MaLoaiPhong == 0 ? currentRoom.MaLoaiPhong : model.MaLoaiPhong);

                await _db.ExecuteAsync(
                    "sp_CapNhatPhong",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                var updateRoom = await _db.QueryFirstOrDefaultAsync<PhongDTO>(
                    "SELECT MaPhong, SoPhong, TinhTrangPhong, MaLoaiPhong, SoGiuong, GhiChu FROM Phong AS p JOIN LoaiPhong AS lp on lp.MaLoaiPhong = p.LoaiPhong WHERE p.MaPhong = @Id",
                    new { Id = id });
                return Ok(updateRoom);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        //DELETE api/Phong/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _db.ExecuteAsync(
                "IF EXISTS (SELECT 1 FROM Phong WHERE MaPhong = @Id AND TinhTrangPhong != 0) DELETE FROM PHONG WHERE MaPhong = @Id", new { Id = id });
            return NoContent();
        }

        [HttpPost("loc-phong")]
        public async Task<IActionResult> LocPhong([FromBody] LocPhongRequest request)
        {
            var parameters = new DynamicParameters();
            // parameters.Add("@TinhTrangPhongId", request.TinhTrangPhong, DbType.Byte);
            parameters.Add("@GiaPhongMin", request.GiaPhongMin, DbType.Int32);
            parameters.Add("@GiaPhongMax", request.GiaPhongMax, DbType.Int32);

            var result = await _db.QueryAsync<LocPhongDtoResponse>(
                "sp_LocPhong",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return Ok(result);
        }
    }
}
