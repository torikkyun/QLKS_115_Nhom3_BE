namespace QLKS_115_Nhom3_BE.DTOs
{
    public class PhongDichVuDTO
    {
        public int MaPhong { get; set; }
        public List<string>? DichVus { get; set; }
        public DateOnly NgayNhanPhong { get; set; }
        public DateOnly NgayTraPhong { get; set; }
    }

    public class DatPhongRequestDTO
    {
        public List<PhongDichVuDTO> PhongDichVus { get; set; }
        public int? KhuyenMaiId { get; set; }
        public string? GhiChu { get; set; }
        public int MaKhachHang { get; set; }
    }
}
