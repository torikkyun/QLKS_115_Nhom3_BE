using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLKS_115_Nhom3_BE.Helpers;
using QLKS_115_Nhom3_BE.Models;
using QLKS_115_Nhom3_BE.DTOs;

namespace QLKS_115_Nhom3_BE.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoaiPhongController : ControllerBase
    {
        private readonly IDbConnection _db;

        public LoaiPhongController(IDbConnection db)
        {
            _db = db;
        }

        // GET: api/LoaiPhong
        [HttpGet]
        public async Task<ActionResult<PagedResult<LoaiPhongDTO>>> Get(int page = 1, int pageSize = 10)
        {
            var sql = "SELECT * FROM LoaiPhong";
            var result = await PaginationHelper.GetPagedDataAsync<LoaiPhongDTO>(_db, sql, page, pageSize);
            return Ok(result);
        }

        // GET api/LoaiPhong/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LoaiPhongDTO>> Get(int id)
        {
            var result = await _db.QueryFirstOrDefaultAsync<LoaiPhongDTO>(
                "SELECT * FROM LoaiPhong WHERE MaLoaiPhong = @Id",
                new { Id = id });

            return result == null ? NotFound() : Ok(result);
        }

        // POST api/LoaiPhong
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateLoaiPhongDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var sql = @"INSERT INTO LoaiPhong (SoGiuong, GhiChu) 
                        VALUES (@SoGiuong, @GhiChu);
                        SELECT CAST(SCOPE_IDENTITY() as int)";

                var id = await _db.ExecuteScalarAsync<int>(sql, model);
                return CreatedAtAction(nameof(Get), new { id }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
        // PUT api/LoaiPhong/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateLoaiPhongDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var sql = @"UPDATE LoaiPhong SET 
                        SoGiuong = @SoGiuong, GhiChu = @Ghichu
                        WHERE MaLoaiPhong = @Id";

                await _db.ExecuteAsync(sql, new
                {
                    model.SoGiuong,
                    model.GhiChu,
                    Id = id
                });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        // DELETE api/LoaiPhong/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _db.ExecuteAsync(
                "DELETE FROM LoaiPhong WHERE MaLoaiPhong = @Id", new { Id = id });
            return NoContent();
        }

    }
}