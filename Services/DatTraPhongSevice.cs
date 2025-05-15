using Dapper;
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
        Task<IEnumerable<DatPhongFullDTO>> GetAllDatPhongsAsync();

    }
    public class DatPhongService : IDatPhongService
    {
        private readonly DataQlks115Nhom3Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HoaDonService _hoaDonService;
        private readonly string _connectionString;

        public DatPhongService(DataQlks115Nhom3Context context, IHttpContextAccessor httpContextAccessor, HoaDonService hoaDonService, IConfiguration configuration)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _hoaDonService = hoaDonService;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
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

                    // 3.3. Xử lý dịch vụ nếu có (sửa lại phần này)
                    if (phongDv.DichVuIds != null && phongDv.DichVuIds.Any())
                    {
                        // Lấy danh sách dịch vụ hợp lệ
                        var validDichVus = await _context.DichVus
                            .Where(dv => phongDv.DichVuIds.Contains(dv.MaDichVu))
                            .ToListAsync();

                        // Kiểm tra xem có dịch vụ không tồn tại không
                        var invalidIds = phongDv.DichVuIds.Except(validDichVus.Select(dv => dv.MaDichVu));
                        if (invalidIds.Any())
                            throw new Exception($"Không tìm thấy dịch vụ với mã: {string.Join(", ", invalidIds)}");

                        foreach (var dichVu in validDichVus)
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



        public async Task<IEnumerable<DatPhongFullDTO>> GetAllDatPhongsAsync()
        {
            var query = await _context.DatPhongs
                .Include(dp => dp.NhanVienNavigation)
                .Include(dp => dp.KhachHangNavigation)
                .OrderByDescending(dp => dp.MaDatPhong)
                .ToListAsync();

            var result = query.Select(dp => new DatPhongFullDTO
            {
                MaDatPhong = dp.MaDatPhong,
                NgayDatPhong = dp.NgayDatPhong,
                SoPhongDat = dp.SoPhongDat,
                GhiChu = dp.GhiChu,

                NhanVien = new NhanVienDTO
                {
                    MaNhanVien = dp.NhanVienNavigation.MaNhanVien,
                    Ho = dp.NhanVienNavigation.Ho,
                    Ten = dp.NhanVienNavigation.Ten,
                    Email = dp.NhanVienNavigation.Email,
                    Sdt = dp.NhanVienNavigation.Sdt,
                    Cccd = dp.NhanVienNavigation.Cccd,
                    VaiTro = dp.NhanVienNavigation.VaiTro
                },

                KhachHang = new KhachHangDTO
                {
                    MaKhachHang = dp.KhachHangNavigation.MaKhachHang,
                    Ho = dp.KhachHangNavigation.Ho,
                    Ten = dp.KhachHangNavigation.Ten,
                    Email = dp.KhachHangNavigation.Email,
                    Sdt = dp.KhachHangNavigation.Sdt,
                    Cccd = dp.KhachHangNavigation.Cccd
                }
            });

            return result;
        }
    }
}
