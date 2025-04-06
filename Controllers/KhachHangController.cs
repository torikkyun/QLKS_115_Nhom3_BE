using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QLKS_115_Nhom3_BE.Helpers;
using QLKS_115_Nhom3_BE.Models;

namespace QLKS_115_Nhom3_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly IDbConnection _db;

        public KhachHangController(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        // GET: api/KhachHang
        [HttpGet]
        public async Task<ActionResult<PagedResult<KhachHangDTO>>> Get(int page = 1, int pageSize = 10)
        {
            var sql = "SELECT * FROM KhachHang";
            var result = await PaginationHelper.GetPagedDataAsync<KhachHangDTO>(_db, sql, page, pageSize);
            return Ok(result);
        }

        // GET api/KhachHang/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<KhachHangDTO>> Get(int id)
        {
            var result = await _db.QueryFirstOrDefaultAsync<KhachHangDTO>(
                "SELECT * FROM KhachHang WHERE MaKhachHang = @Id",
                new { Id = id });

            return result == null ? NotFound() : Ok(result);
        }

        // POST api/KhachHang
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateKhachHangDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var checkSql = "SELECT COUNT(*) FROM KhachHang WHERE Cccd = @Cccd";
                var exists = await _db.ExecuteScalarAsync<int>(checkSql, new { model.Cccd });

                if (exists > 0)
                {
                    return Conflict(new { message = "CCCD đã tồn tại trong hệ thống." });
                }

                var sql = @"INSERT INTO KhachHang (Ho, Ten, Email, Sdt, Cccd) 
                        VALUES (@Ho, @Ten, @Email, @Sdt, @Cccd);
                        SELECT CAST(SCOPE_IDENTITY() as int)";

                var id = await _db.ExecuteScalarAsync<int>(sql, model);
                return CreatedAtAction(nameof(Get), new { id }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        // PUT api/KhachHang/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateKhachHangDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var sql = @"UPDATE KhachHang SET 
                        Ho = @Ho, Ten = @Ten, Email = @Email, 
                        Sdt = @Sdt, Cccd = @Cccd 
                        WHERE MaKhachHang = @Id";

                await _db.ExecuteAsync(sql, new
                {
                    model.Ho,
                    model.Ten,
                    model.Email,
                    model.Sdt,
                    model.Cccd,
                    Id = id
                });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        // DELETE api/KhachHang/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _db.ExecuteAsync(
                "DELETE FROM KhachHang WHERE MaKhachHang = @Id", new { Id = id });
            return NoContent();
        }
    }
}