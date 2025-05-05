using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QLKS_115_Nhom3_BE.DTOs;
using QLKS_115_Nhom3_BE.Models;
using System.Data;


namespace QLKS_115_Nhom3_BE.Services
{
    public interface IDatPhongService
    {
        Task<int> DatPhongAsync(DatPhongRequestDTO request);
        Task<bool> TraPhongAsync(TraPhongRequestDTO request);
    }
    public class DatPhongService : IDatPhongService
    {
        private readonly DataQlks115Nhom3Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HoaDonService _hoaDonService;

        public DatPhongService(DataQlks115Nhom3Context context, IHttpContextAccessor httpContextAccessor, HoaDonService hoaDonService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _hoaDonService = hoaDonService;
        }

        public async Task<int> DatPhongAsync(DatPhongRequestDTO request)
        {
            var nhanVien = _httpContextAccessor.HttpContext?.Items["CurrentNhanVien"] as NhanVien;
            if (nhanVien == null)
                throw new Exception("Không xác định được nhân viên");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Kiểm tra và lấy danh sách phòng
                var maPhongs = request.PhongDichVus.Select(p => p.MaPhong).ToList();
                var phongs = await _context.Phongs
                    .Where(p => maPhongs.Contains(p.MaPhong))
                    .ToListAsync();

                if (phongs.Count != maPhongs.Count)
                    throw new Exception("Một số phòng không tồn tại");

                // 2. Tạo đặt phòng chính
                var datPhong = new DatPhong
                {
                    NgayDatPhong = DateOnly.FromDateTime(DateTime.Now),
                    SoPhongDat = request.PhongDichVus.Count,
                    GhiChu = request.GhiChu,
                    NhanVien = nhanVien.MaNhanVien,
                    KhachHang = request.MaKhachHang
                };
                _context.DatPhongs.Add(datPhong);
                await _context.SaveChangesAsync();

                // 3. Xử lý từng phòng và dịch vụ
                foreach (var phongDv in request.PhongDichVus)
                {
                    // 3.1. Kiểm tra trạng thái phòng
                    var phong = phongs.First(p => p.MaPhong == phongDv.MaPhong);
                    if (phong.TinhTrangPhong != 1)
                        throw new Exception($"Phòng {phong.SoPhong} không khả dụng");

                    // 3.2. Tạo chi tiết đặt phòng
                    var chiTietDatPhong = new ChiTietDatPhong
                    {
                        Phong = phongDv.MaPhong,
                        DatPhong = datPhong.MaDatPhong,
                        KhuyenMai = request.KhuyenMaiId,
                        NgayNhanPhong = phongDv.NgayNhanPhong,
                        NgayTraPhong = phongDv.NgayTraPhong
                    };
                    _context.ChiTietDatPhongs.Add(chiTietDatPhong);

                    // 3.3. Xử lý dịch vụ nếu có
                    if (phongDv.DichVus != null && phongDv.DichVus.Any())
                    {
                        var dichVus = await _context.DichVus
                            .Where(dv => phongDv.DichVus.Contains(dv.TenDichVu))
                            .ToListAsync();

                        if (dichVus.Count != phongDv.DichVus.Count)
                            throw new Exception($"Một số dịch vụ không tồn tại cho phòng {phongDv.MaPhong}");

                        foreach (var dichVu in dichVus)
                        {
                            _context.ChiTietDichVus.Add(new ChiTietDichVu
                            {
                                Phong = phongDv.MaPhong,
                                DatPhong = datPhong.MaDatPhong,
                                DichVu = dichVu.MaDichVu,
                                NgaySuDung = phongDv.NgayNhanPhong
                            });
                        }
                    }

                    // 3.4. Cập nhật trạng thái phòng
                    phong.TinhTrangPhong = 0;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                await _hoaDonService.CreateHoaDonAsync(datPhong.MaDatPhong);

                return datPhong.MaDatPhong;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Lỗi khi đặt phòng: {ex.Message}");
            }
        }
        public async Task<bool> TraPhongAsync(TraPhongRequestDTO request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. Kiểm tra hóa đơn của mã đặt phòng đã thanh toán chưa
                var hoaDon = await _context.HoaDons
                    .FirstOrDefaultAsync(h => h.DatPhong == request.MaDatPhong && h.TinhTrangThanhToan == 2);

                if (hoaDon == null)
                    return false; // Hóa đơn chưa thanh toán hoặc không tồn tại

                // 2. Tìm phòng và cập nhật trạng thái về 1 (trống)
                var phong = await _context.Phongs.FindAsync(request.MaPhong);
                if (phong == null)
                    throw new Exception("Phòng không tồn tại.");

                phong.TinhTrangPhong = 1;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
