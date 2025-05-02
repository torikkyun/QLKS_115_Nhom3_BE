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
    public class KhuyenMaiController : ControllerBase
    {
        private readonly IDbConnection _db;

        public KhuyenMaiController(IDbConnection db)
        {
            _db = db;
        }
        // GET: api/KhuyenMai
        [HttpGet]
        public async Task<ActionResult<PagedResult<KhuyenMaiDTO>>> Get(int page = 1, int pageSize = 10)
        {
            var sql = @"SELECT km.*, kkm.TenKieu AS TenKieuKhuyenMai
                FROM KhuyenMai km
                LEFT JOIN KieuKhuyenMaiEnum kkm ON km.KieuKhuyenMai = kkm.Id";
            var result = await PaginationHelper.GetPagedDataAsync<KhuyenMaiDTO>(_db, sql, page, pageSize);
            return Ok(result);
        }

        // GET api/KhuyenMai/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<KhuyenMaiDTO>> Get(int id)
        {
            var result = await _db.QueryFirstOrDefaultAsync<KhuyenMaiDTO>(
                @"SELECT km.*, kkm.TenKieu AS TenKieuKhuyenMai
                FROM KhuyenMai km
                LEFT JOIN KieuKhuyenMaiEnum kkm ON km.KieuKhuyenMai = kkm.Id
                WHERE km.MaKhuyenMai = @Id",
                new { Id = id });

            return result == null ? NotFound() : Ok(result);
        }

        // POST api/KhuyenMai
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateKhuyenMaiDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var sql = @"INSERT INTO KhuyenMai (TenKhuyenMai, MoTaKhuyenMai, KieuKhuyenMai, NgayBatDau, NgayKetThuc, GiaTriKhuyenMai, GhiChu) 
                        VALUES (@TenKhuyenMai, @MoTaKhuyenMai, @KieuKhuyenMai, @NgayBatDau, @NgayKetThuc, @GiaTriKhuyenMai, @GhiChu);
                        SELECT CAST(SCOPE_IDENTITY() as int)";

                var id = await _db.ExecuteScalarAsync<int>(sql, model);
                return CreatedAtAction(nameof(Get), new { id }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
        // PUT api/KhuyenMai/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateKhuyenMaiDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var sql = @"UPDATE KhuyenMai SET 
                        TenKhuyenMai = @TenKhuyenMai, MoTaKhuyenMai = @MoTaKhuyenMai, KieuKhuyenMai = @KieuKhuyenMai, NgayBatDau = @NgayBatDau, NgayKetThuc = @NgayKetThuc, GiaTriKhuyenMai = @GiaTriKhuyenMai, GhiChu = @Ghichu
                        WHERE MaKhuyenMai = @Id";

                await _db.ExecuteAsync(sql, new
                {
                    model.TenKhuyenMai,
                    model.MoTaKhuyenMai,
                    model.KieuKhuyenMai,
                    model.NgayBatDau,
                    model.NgayKetThuc,
                    model.GiaTriKhuyenMai,
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

        // DELETE api/KhuyenMai/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _db.ExecuteAsync(
                "DELETE FROM KhuyenMai WHERE MaKhuyenMai = @Id", new { Id = id });
            return NoContent();
        }
    }
}