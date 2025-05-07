namespace QLKS_115_Nhom3_BE.DTOs
{
    public class DatPhongTrongHoaDonDTO
    {
        public int MaDatPhong { get; set; }
        public DateTime NgayDatPhong { get; set; }
        public int SoPhongDat { get; set; }
        public string? GhiChu { get; set; }
        public KhachHangTrongHoaDonDTO? KhachHang { get; set; }
    }
    public class NhanVienTrongHoaDonDTO
    {
        public int MaNhanVien { get; set; }
        public string Ho { get; set; } = null!;
        public string Ten { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Sdt { get; set; } = null!;
        public string Cccd { get; set; } = null!;
        public int VaiTro { get; set; }
    }

    public class KhachHangTrongHoaDonDTO
    {
        public int MaKhachHang { get; set; }
        public string Ho { get; set; } = null!;
        public string Ten { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Sdt { get; set; } = null!;
        public string Cccd { get; set; } = null!;
    }

    public class TinhTrangThanhToanDTO
    {
        public byte Id { get; set; }
        public string TenTinhTrang { get; set; } = null!;
    }

    public class HoaDonDTO
    {
        public DatPhongTrongHoaDonDTO DatPhong { get; set; } = null!;
        public DateTime NgayXuatHoaDon { get; set; }
        public int TongTien { get; set; }
        public int TongTienPhong { get; set; }
        public int TongTienDichVu { get; set; }
        public TinhTrangThanhToanDTO TinhTrangThanhToan { get; set; } = null!;
        public NhanVienTrongHoaDonDTO? NhanVien { get; set; }
    }
}