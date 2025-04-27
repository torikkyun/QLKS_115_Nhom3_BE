using Microsoft.EntityFrameworkCore;
using QLKS_115_Nhom3_BE.DTOs;
using QLKS_115_Nhom3_BE.Models;
using QLKS_115_Nhom3_BE.Utilities;
using System.Data;
using System.Security.Claims;


namespace QLKS_115_Nhom3_BE.Services
{
    public interface IDatPhongService
    {
        Task<int> DatPhongAsync(DatPhongRequestDTO request);
    }
    public class DatPhongService : IDatPhongService
    {
        private readonly DataQlks115Nhom3Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DatPhongService(DataQlks115Nhom3Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> DatPhongAsync(DatPhongRequestDTO request)
        {
            var nhanVien = _httpContextAccessor.HttpContext?.Items["CurrentNhanVien"] as NhanVien;
            if (nhanVien == null)
                throw new Exception("Không xác định được nhân viên");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Kiểm tra tất cả phòng có sẵn không
                var phongs = await _context.Phongs
                    .Where(p => request.MaPhongs.Contains(p.MaPhong))
                    .ToListAsync();

                if (phongs.Count != request.MaPhongs.Count)
                    throw new Exception("Một số phòng không tồn tại");

                var phongKhongKhaDung = phongs.FirstOrDefault(p => p.TinhTrangPhong != 1);
                if (phongKhongKhaDung != null)
                    throw new Exception($"Phòng {phongKhongKhaDung.SoPhong} không khả dụng");

                // Tạo đặt phòng
                var datPhong = new DatPhong
                {
                    NgayDatPhong = DateOnly.FromDateTime(DateTime.Now),
                    SoPhongDat = request.MaPhongs.Count, // Số lượng phòng đặt
                    GhiChu = request.GhiChu,
                    NhanVien = nhanVien.MaNhanVien,
                    KhachHang = request.MaKhachHang
                };

                _context.DatPhongs.Add(datPhong);
                await _context.SaveChangesAsync();

                // Tạo chi tiết cho từng phòng
                foreach (var maPhong in request.MaPhongs)
                {
                    var chiTiet = new ChiTietDatPhong
                    {
                        Phong = maPhong,
                        DatPhong = datPhong.MaDatPhong,
                        KhuyenMai = request.KhuyenMaiId,
                        NgayNhanPhong = request.NgayNhanPhong,
                        NgayTraPhong = request.NgayTraPhong
                    };
                    _context.ChiTietDatPhongs.Add(chiTiet);

                    // Cập nhật trạng thái phòng
                    var phong = phongs.First(p => p.MaPhong == maPhong);
                    phong.TinhTrangPhong = 0;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return datPhong.MaDatPhong;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
