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
            // Lấy thông tin nhân viên từ context
            var nhanVien = _httpContextAccessor.HttpContext?.Items["CurrentNhanVien"] as NhanVien;

            if (nhanVien == null)
            {
                throw new Exception("Không xác định được nhân viên");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Kiểm tra phòng có sẵn không
                var phong = await _context.Phongs
                    .Include(p => p.TinhTrangPhongNavigation)
                    .FirstOrDefaultAsync(p => p.MaPhong == request.MaPhong);

                if (phong == null)
                    throw new Exception("Phòng không tồn tại");

                if (phong.TinhTrangPhong != 1) // 1 = Trống
                    throw new Exception("Phòng không khả dụng để đặt");

                // Bước 1: Tạo đặt phòng
                var datPhong = new DatPhong
                {
                    NgayDatPhong = DateOnly.FromDateTime(DateTime.Now),
                    SoPhongDat = request.MaPhong,
                    GhiChu = request.GhiChu,
                    NhanVien = nhanVien.MaNhanVien,
                    KhachHang = request.MaKhachHang, // Có thể thêm logic tìm hoặc tạo khách hàng
                };

                _context.DatPhongs.Add(datPhong);
                await _context.SaveChangesAsync();

                // Bước 2: Tạo chi tiết đặt phòng
                var chiTiet = new ChiTietDatPhong
                {
                    Phong = request.MaPhong,
                    DatPhong = datPhong.MaDatPhong,
                    KhuyenMai = request.KhuyenMaiId,
                    NgayNhanPhong = request.NgayNhanPhong,
                    NgayTraPhong = request.NgayTraPhong
                };

                _context.ChiTietDatPhongs.Add(chiTiet);

                // Bước 3: Cập nhật trạng thái phòng
                phong.TinhTrangPhong = 0; // 0 = Đang sử dụng
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

        // Implement other methods here...
    }
}
