using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLKS_115_Nhom3_BE.Helpers;
using QLKS_115_Nhom3_BE.Models;

namespace QLKS_115_Nhom3_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaiTroController : ControllerBase
    {
        private readonly IDbConnection _db;

        public VaiTroController(IDbConnection db)
        {
            _db = db;
        }

        // GET: api/VaiTro
        [HttpGet]
        public async Task<ActionResult<PagedResult<VaiTroDTO>>> Get(int page = 1, int pageSize = 10)
        {
            var sql = "SELECT * FROM VaiTro";
            var result = await PaginationHelper.GetPagedDataAsync<VaiTroDTO>(_db, sql, page, pageSize);
            return Ok(result);
        }

        // GET api/VaiTro/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<VaiTroDTO>> Get(int id)
        {
            var result = await _db.QueryFirstOrDefaultAsync<VaiTroDTO>(
                "SELECT * FROM VaiTro WHERE MaVaiTro = @Id",
                new { Id = id });

            return result == null ? NotFound() : Ok(result);
        }

        // POST api/VaiTro
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateVaiTroDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var sql = @"INSERT INTO VaiTro (TenVaiTro, GhiChu) 
                        VALUES (@TenVaiTro, @GhiChu);
                        SELECT CAST(SCOPE_IDENTITY() as int)";

                var id = await _db.ExecuteScalarAsync<int>(sql, model);
                return CreatedAtAction(nameof(Get), new { id }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
        // PUT api/VaiTro/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateVaiTroDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var sql = @"UPDATE VaiTro SET 
                        TenVaiTro = @TenVaiTro, GhiChu = @Ghichu
                        WHERE MaVaiTro = @Id";

                await _db.ExecuteAsync(sql, new
                {
                    model.TenVaiTro,
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

        // DELETE api/VaiTro/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _db.ExecuteAsync(
                "DELETE FROM VaiTro WHERE MaVaiTro = @Id", new { Id = id });
            return NoContent();
        }
    }
}