using QLKS_115_Nhom3_BE.Models;

namespace QLKS_115_Nhom3_BE.DTOs
{
    public class UpdatePhongDTO
    {
        public string SoPhong { get; set; } = null!;

        public int? LoaiPhong { get; set; }
    }
    public class CreatePhongDTO : UpdatePhongDTO
    {
        public byte TinhTrangPhong { get; set; }
    }
    public class PhongDTO : CreatePhongDTO
    {
        // public int MaPhong { get; set; }

        public int SoGiuong { get; set; }

        public string? GhiChu { get; set; }
    }
}
