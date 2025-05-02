namespace QLKS_115_Nhom3_BE.DTOs
{
    public class KhuyenMaiDTO
    {
        public int MaKhuyenMai { get; set; }
        public string TenKhuyenMai { get; set; } = null!;
        public string MoTaKhuyenMai { get; set; } = null!;
        public DateOnly NgayBatDau { get; set; }
        public DateOnly NgayKetThuc { get; set; }
        public int GiaTriKhuyenMai { get; set; }
        public string? GhiChu { get; set; }
        public string TenKieuKhuyenMai { get; set; } = null!;
    }
    public class CreateKhuyenMaiDTO
    {
        public string TenKhuyenMai { get; set; } = null!;
        public string MoTaKhuyenMai { get; set; } = null!;
        public byte KieuKhuyenMai { get; set; }
        public DateOnly NgayBatDau { get; set; }
        public DateOnly NgayKetThuc { get; set; }
        public int GiaTriKhuyenMai { get; set; }
        public string? GhiChu { get; set; }
    }
    public class UpdateKhuyenMaiDTO
    {
        public string? TenKhuyenMai { get; set; }
        public string? MoTaKhuyenMai { get; set; }
        public byte? KieuKhuyenMai { get; set; }
        public DateOnly? NgayBatDau { get; set; }
        public DateOnly? NgayKetThuc { get; set; }
        public int? GiaTriKhuyenMai { get; set; }
        public string? GhiChu { get; set; }
    }
}