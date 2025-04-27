namespace QLKS_115_Nhom3_BE.DTOs
{
    public class DatPhongRequestDTO
    {
        public List<int> MaPhongs { get; set; }
        public DateOnly NgayNhanPhong { get; set; }
        public DateOnly NgayTraPhong { get; set; }
        public int? KhuyenMaiId { get; set; }
        public string? GhiChu { get; set; }
        public int MaKhachHang { get; set; }
    }
}
