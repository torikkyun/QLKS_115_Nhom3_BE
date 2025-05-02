namespace QLKS_115_Nhom3_BE.DTOs
{
    public class HoaDonDTO
    {
        public int DatPhong { get; set; }
        public DateOnly NgayXuatHoaDon { get; set; }
        public byte TinhTrangThanhToan { get; set; }
        public string TenTinhTrangThanhToan { get; set; } = null!;
        public int TongTien { get; set; }
        public int TongTienPhong { get; set; }
        public int TongTienDichVu { get; set; }
        public string? GhiChu { get; set; }
        public int? NhanVien { get; set; }
    }
}