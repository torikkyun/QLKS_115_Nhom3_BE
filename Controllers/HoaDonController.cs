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
            var sql = @"
                SELECT 
                    hd.DatPhong as MaDatPhong,
                    dp.NgayDatPhong,
                    dp.SoPhongDat,
                    dp.GhiChu,
                    hd.NgayXuatHoaDon,
                    hd.TongTien,
                    hd.TongTienPhong,
                    hd.TongTienDichVu,
                    hd.TinhTrangThanhToan as Id,
                    tt.TenTinhTrang,
                    nv.MaNhanVien,
                    nv.Ho,
                    nv.Ten,
                    nv.Email,
                    nv.SDT as Sdt,
                    nv.CCCD as Cccd,
                    nv.VaiTro,
                    kh.MaKhachHang,
                    kh.Ho as KhachHangHo,
                    kh.Ten as KhachHangTen,
                    kh.Email as KhachHangEmail,
                    kh.SDT as KhachHangSdt,
                    kh.CCCD as KhachHangCccd
                FROM HoaDon hd
                LEFT JOIN DatPhong dp ON hd.DatPhong = dp.MaDatPhong
                LEFT JOIN NhanVien nv ON hd.NhanVien = nv.MaNhanVien
                LEFT JOIN TinhTrangThanhToanEnum tt ON hd.TinhTrangThanhToan = tt.Id
                LEFT JOIN KhachHang kh ON dp.KhachHang = kh.MaKhachHang
            ";

            using var multi = await _db.QueryMultipleAsync(sql + " ORDER BY hd.DatPhong OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY; SELECT COUNT(*) FROM HoaDon;",
                new { Offset = (page - 1) * pageSize, PageSize = pageSize });

            var hoaDons = (await multi.ReadAsync<dynamic>()).Select(row => new HoaDonDTO
            {
                DatPhong = new DatPhongTrongHoaDonDTO
                {
                    MaDatPhong = row.MaDatPhong,
                    NgayDatPhong = row.NgayDatPhong,
                    SoPhongDat = row.SoPhongDat,
                    GhiChu = row.GhiChu,
                    KhachHang = row.MaKhachHang == null ? null : new KhachHangTrongHoaDonDTO
                    {
                        MaKhachHang = row.MaKhachHang,
                        Ho = row.KhachHangHo,
                        Ten = row.KhachHangTen,
                        Email = row.KhachHangEmail,
                        Sdt = row.KhachHangSdt,
                        Cccd = row.KhachHangCccd
                    }
                },
                NgayXuatHoaDon = row.NgayXuatHoaDon,
                TongTien = row.TongTien,
                TongTienPhong = row.TongTienPhong,
                TongTienDichVu = row.TongTienDichVu,
                TinhTrangThanhToan = new TinhTrangThanhToanDTO
                {
                    Id = row.Id,
                    TenTinhTrang = row.TenTinhTrang
                },
                NhanVien = row.MaNhanVien == null ? null : new NhanVienTrongHoaDonDTO
                {
                    MaNhanVien = row.MaNhanVien,
                    Ho = row.Ho,
                    Ten = row.Ten,
                    Email = row.Email,
                    Sdt = row.Sdt,
                    Cccd = row.Cccd,
                    VaiTro = row.VaiTro
                }
            }).ToList();

            var totalCount = await multi.ReadFirstAsync<int>();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var result = new PagedResult<HoaDonDTO>
            {
                Data = hoaDons,
                TotalRecords = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages
            };

            return Ok(result);
        }

        // GET api/HoaDon/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HoaDonDTO>> Get(int id)
        {
            var sql = @"
                SELECT 
                    hd.DatPhong as MaDatPhong,
                    dp.NgayDatPhong,
                    dp.SoPhongDat,
                    dp.GhiChu,
                    hd.NgayXuatHoaDon,
                    hd.TongTien,
                    hd.TongTienPhong,
                    hd.TongTienDichVu,
                    hd.TinhTrangThanhToan as Id,
                    tt.TenTinhTrang,
                    nv.MaNhanVien,
                    nv.Ho,
                    nv.Ten,
                    nv.Email,
                    nv.SDT as Sdt,
                    nv.CCCD as Cccd,
                    nv.VaiTro,
                    kh.MaKhachHang,
                    kh.Ho as KhachHangHo,
                    kh.Ten as KhachHangTen,
                    kh.Email as KhachHangEmail,
                    kh.SDT as KhachHangSdt,
                    kh.CCCD as KhachHangCccd
                FROM HoaDon hd
                LEFT JOIN DatPhong dp ON hd.DatPhong = dp.MaDatPhong
                LEFT JOIN NhanVien nv ON hd.NhanVien = nv.MaNhanVien
                LEFT JOIN TinhTrangThanhToanEnum tt ON hd.TinhTrangThanhToan = tt.Id
                LEFT JOIN KhachHang kh ON dp.KhachHang = kh.MaKhachHang
                WHERE hd.DatPhong = @Id";

            var result = await _db.QueryFirstOrDefaultAsync<dynamic>(sql, new { Id = id });

            if (result == null)
                return NotFound();

            var hoaDon = new HoaDonDTO
            {
                DatPhong = new DatPhongTrongHoaDonDTO
                {
                    MaDatPhong = result.MaDatPhong,
                    NgayDatPhong = result.NgayDatPhong,
                    SoPhongDat = result.SoPhongDat,
                    GhiChu = result.GhiChu,
                    KhachHang = result.MaKhachHang == null ? null : new KhachHangTrongHoaDonDTO
                    {
                        MaKhachHang = result.MaKhachHang,
                        Ho = result.KhachHangHo,
                        Ten = result.KhachHangTen,
                        Email = result.KhachHangEmail,
                        Sdt = result.KhachHangSdt,
                        Cccd = result.KhachHangCccd
                    }
                },
                NgayXuatHoaDon = result.NgayXuatHoaDon,
                TongTien = result.TongTien,
                TongTienPhong = result.TongTienPhong,
                TongTienDichVu = result.TongTienDichVu,
                TinhTrangThanhToan = new TinhTrangThanhToanDTO
                {
                    Id = result.Id,
                    TenTinhTrang = result.TenTinhTrang
                },
                NhanVien = result.MaNhanVien == null ? null : new NhanVienTrongHoaDonDTO
                {
                    MaNhanVien = result.MaNhanVien,
                    Ho = result.Ho,
                    Ten = result.Ten,
                    Email = result.Email,
                    Sdt = result.Sdt,
                    Cccd = result.Cccd,
                    VaiTro = result.VaiTro
                }
            };

            return Ok(hoaDon);
        }
    }
}