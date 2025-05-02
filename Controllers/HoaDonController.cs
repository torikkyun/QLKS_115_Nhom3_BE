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
    public class HoaDonController : ControllerBase
    {
        private readonly IDbConnection _db;

        public HoaDonController(IDbConnection db)
        {
            _db = db;
        }

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
    }
}