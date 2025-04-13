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
    public class DichVuController : ControllerBase
    {

        private readonly IDbConnection _db;
        public DichVuController(IDbConnection db)
        {
            _db = db;
        }
        // GET: api/DichVu
        [HttpGet]
        public async Task<ActionResult<PagedResult<DichVuDTO>>> Get(int page = 1, int pageSize = 10)
        {
            var sql = "SELECT * FROM DichVu";
            var result = await PaginationHelper.GetPagedDataAsync<DichVuDTO>(_db, sql, page, pageSize);
            return Ok(result);
        }

        // GET api/DichVu/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DichVuDTO>> Get(int id)
        {
            var result = await _db.QueryFirstOrDefaultAsync<DichVuDTO>(
                "SELECT * FROM DichVu WHERE MaDichVu = @Id",
                new { Id = id });

            return result == null ? NotFound() : Ok(result);
        }

        // POST api/DichVu
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDichVuDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var sql = @"INSERT INTO DichVu (TenDichVu, LoaiDichVu, GhiChu) 
                        VALUES (@TenDichVu, @LoaiDichVu, @GhiChu);
                        SELECT CAST(SCOPE_IDENTITY() as int)";

                var id = await _db.ExecuteScalarAsync<int>(sql, model);
                return CreatedAtAction(nameof(Get), new { id }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        // PUT api/DichVu/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateDichVuDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var sql = @"UPDATE DichVu SET 
                        TenDichVu = @TenDichVu, LoaiDichVu = @LoaiDichVu, GhiChu = @Ghichu
                        WHERE MaDichVu = @Id";

                await _db.ExecuteAsync(sql, new
                {
                    model.TenDichVu,
                    model.LoaiDichVu,
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

        // DELETE api/DichVu/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _db.ExecuteAsync(
                "DELETE FROM DichVu WHERE MaDichVu = @Id", new { Id = id });
            return NoContent();
        }
    }
}