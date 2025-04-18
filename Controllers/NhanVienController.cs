using Dapper;
using Microsoft.AspNetCore.Mvc;
using QLKS_115_Nhom3_BE.DTOs;
using QLKS_115_Nhom3_BE.Models;
using System.Data;

namespace QLKS_115_Nhom3_BE.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly IDbConnection _db;

        public NhanVienController(IDbConnection db)
        {
            _db = db;
        }
        [HttpGet("all")]
        public async Task<ActionResult<PagedResult<NhanVienDTO>>> Get(int page = 1, int pageSize = 10)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Page", page);
            parameters.Add("@PageSize", pageSize);

            using var multi = await _db.QueryMultipleAsync(
                "sp_LayDanhSachNhanVien",
                param: parameters,
                commandType: CommandType.StoredProcedure
            );

            var data = (await multi.ReadAsync<NhanVienDTO>()).ToList();
            var totalRecords = await multi.ReadFirstAsync<int>();

            var result = new PagedResult<NhanVienDTO>
            {
                TotalRecords = totalRecords,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                Data = data
            };

            return Ok(result);
        }


        // GET api/NhanVien/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<NhanVienDTO>> Get(int id)
        {
            var result = await _db.QueryFirstOrDefaultAsync<NhanVienDTO>(
                "SELECT * FROM NhanVien WHERE MaNhanVien = @Id",
                new { Id = id });

            return result == null ? NotFound() : Ok(result);
        }

        // PUT api/NhanVien/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateNhanVienDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var currentEmployee = await _db.QueryFirstOrDefaultAsync<UpdateNhanVienDTO>(
                                       "SELECT * FROM NhanVien WHERE MaNhanVien = @Id",
                                                          new { Id = id });
                if(currentEmployee == null)
                {
                    return NotFound();
                }

                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                parameters.Add("@Ho", string.IsNullOrWhiteSpace(model.Ho) ? currentEmployee.Ho : model.Ho);
                parameters.Add("@Ten", string.IsNullOrWhiteSpace(model.Ten) ? currentEmployee.Ten : model.Ten);
                parameters.Add("@Email", string.IsNullOrWhiteSpace(model.Email) ? currentEmployee.Email : model.Email);
                parameters.Add("@Sdt", string.IsNullOrWhiteSpace(model.Sdt) ? currentEmployee.Sdt : model.Sdt);
                parameters.Add("@Cccd", string.IsNullOrWhiteSpace(model.Cccd) ? currentEmployee.Cccd : model.Cccd);
                parameters.Add("@MaVaiTro", model.VaiTro == 0 ? currentEmployee.VaiTro : model.VaiTro);


                await _db.ExecuteAsync(
                    "sp_CapNhatNhanVien",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                var updateEmployee = await _db.QueryFirstOrDefaultAsync<UpdateNhanVienDTO>(
                                       "SELECT * FROM NhanVien WHERE MaNhanVien = @Id",
                                                          new { Id = id });
                return Ok(updateEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        // DELETE api/NhanVien/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _db.ExecuteAsync(
                "DELETE FROM NhanVien WHERE MaNhanVien = @Id", new { Id = id });
            return NoContent();
        }
    }
}
