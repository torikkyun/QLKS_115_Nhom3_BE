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
    public class HoaDonController : ControllerBase
    {
        private readonly IDbConnection _db;

        public HoaDonController(IDbConnection db)
        {
            _db = db;
        }

        // POST: api/HoaDon/ThanhToan/{maDatPhong}
        [HttpPost("ThanhToan/{maDatPhong}")]
        public async Task<IActionResult> ThanhToanHoaDon(int maDatPhong)
        {
            try
            {
                // Kiểm tra hóa đơn tồn tại
                var hoaDon = await _db.QueryFirstOrDefaultAsync<dynamic>(
                    @"SELECT h.*, tt.TenTinhTrang 
                    FROM HoaDon h
                    JOIN TinhTrangThanhToanEnum tt ON h.TinhTrangThanhToan = tt.Id
                    WHERE h.DatPhong = @MaDatPhong",
                    new { MaDatPhong = maDatPhong }
                );

                if (hoaDon == null)
                {
                    return NotFound("Không tìm thấy hóa đơn");
                }

                if (hoaDon.TinhTrangThanhToan == 2) // 2 = Đã thanh toán
                {
                    return BadRequest("Hóa đơn này đã được thanh toán");
                }

                // Cập nhật trạng thái hóa đơn
                var result = await _db.ExecuteAsync(
                    @"UPDATE HoaDon 
                    SET TinhTrangThanhToan = 2, -- 2 = Đã thanh toán
                        NgayXuatHoaDon = @NgayXuatHoaDon
                    WHERE DatPhong = @MaDatPhong",
                    new
                    {
                        MaDatPhong = maDatPhong,
                        NgayXuatHoaDon = DateTime.Now
                    }
                );

                if (result > 0)
                {
                    return Ok(new { Message = "Thanh toán hóa đơn thành công" });
                }
                else
                {
                    return BadRequest("Không thể thanh toán hóa đơn");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        // GET: api/HoaDon
        [HttpGet]
        public async Task<ActionResult<PagedResult<HoaDonDTO>>> Get(int page = 1, int pageSize = 10)
        {
            var sql = "SELECT * FROM HoaDon";
            var result = await PaginationHelper.GetPagedDataAsync<HoaDonDTO>(_db, sql, page, pageSize);
            return Ok(result);
        }

        // GET api/HoaDon/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HoaDonDTO>> Get(int id)
        {
            var result = await _db.QueryFirstOrDefaultAsync<HoaDonDTO>(
                "SELECT * FROM HoaDon WHERE DatPhong = @Id",
                new { Id = id });

            return result == null ? NotFound() : Ok(result);
        }
    }
}