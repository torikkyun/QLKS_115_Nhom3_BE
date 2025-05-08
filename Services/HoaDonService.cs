using Dapper;
using System.Data;
using QLKS_115_Nhom3_BE.Utilities;

namespace QLKS_115_Nhom3_BE.Services
{
    public class HoaDonService
    {
        private readonly IDbConnection _db;

        public HoaDonService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<int> CreateHoaDonAsync(int maDatPhong)
        {
            const string sql = @"
                -- Tính tổng tiền phòng gốc
                DECLARE @TongTienPhong INT;
                SELECT @TongTienPhong = SUM(lp.GiaPhong * (DATEDIFF(DAY, ctdp.NgayNhanPhong, ctdp.NgayTraPhong) + 1))
                FROM ChiTietDatPhong ctdp
                JOIN Phong p ON ctdp.Phong = p.MaPhong
                JOIN LoaiPhong lp ON p.LoaiPhong = lp.MaLoaiPhong
                WHERE ctdp.DatPhong = @MaDatPhong;

                -- Tính tổng tiền giảm từ khuyến mãi
                DECLARE @TongTienGiam INT = 0;
                SELECT @TongTienGiam = COALESCE(SUM(
                    CASE km.KieuKhuyenMai
                        WHEN 1 THEN (lp.GiaPhong * (DATEDIFF(DAY, ctdp.NgayNhanPhong, ctdp.NgayTraPhong) + 1)) * km.GiaTriKhuyenMai / 100 -- Phần trăm
                        WHEN 2 THEN km.GiaTriKhuyenMai -- Giảm trực tiếp
                        ELSE 0 -- Bỏ qua loại 3 (đặc biệt)
                    END
                ), 0)
                FROM ChiTietDatPhong ctdp
                JOIN Phong p ON ctdp.Phong = p.MaPhong
                JOIN LoaiPhong lp ON p.LoaiPhong = lp.MaLoaiPhong
                JOIN KhuyenMai km ON ctdp.KhuyenMai = km.MaKhuyenMai
                WHERE ctdp.DatPhong = @MaDatPhong;

                -- Tính tổng tiền phòng sau khuyến mãi
                SET @TongTienPhong = @TongTienPhong - @TongTienGiam;

                -- Tính tổng tiền dịch vụ
                DECLARE @TongTienDichVu INT;
                SELECT @TongTienDichVu = COALESCE(SUM(ldv.GiaDichVu), 0)
                FROM ChiTietDichVu ctdv
                JOIN DichVu dv ON ctdv.DichVu = dv.MaDichVu
                JOIN LoaiDichVuEnum ldv ON dv.LoaiDichVu = ldv.Id
                WHERE ctdv.DatPhong = @MaDatPhong;

                -- Tổng tiền hóa đơn
                DECLARE @TongTien INT = @TongTienPhong + @TongTienDichVu;

                -- Lấy thông tin NhanVien và GhiChu từ DatPhong
                DECLARE @NhanVien INT;
                DECLARE @GhiChu NVARCHAR(100);
                SELECT @NhanVien = dp.NhanVien, @GhiChu = dp.GhiChu
                FROM DatPhong dp
                WHERE dp.MaDatPhong = @MaDatPhong;

                -- Tạo hóa đơn
                INSERT INTO HoaDon (DatPhong, TongTien, TongTienPhong, TongTienDichVu, TinhTrangThanhToan, NhanVien, GhiChu)
                VALUES (@MaDatPhong, @TongTien, @TongTienPhong, @TongTienDichVu, 1, @NhanVien, @GhiChu);

                SELECT SCOPE_IDENTITY();
            ";

            var parameters = new { MaDatPhong = maDatPhong };

            try
            {
                var hoaDonId = await _db.ExecuteScalarAsync<int>(sql, parameters);
                return hoaDonId;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tạo hóa đơn", ex);
            }
        }
    }
}
