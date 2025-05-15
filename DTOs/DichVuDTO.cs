namespace QLKS_115_Nhom3_BE.DTOs
{

    public class DichVuDTO
    {
        public int MaDichVu { get; set; }
        public string TenDichVu { get; set; } = string.Empty;
        public string? GhiChu { get; set; }
        public string? TenLoaiDichVu { get; set; }
        public int GiaDichVu { get; set; }
    }

    public class CreateDichVuDTO
    {
        public string TenDichVu { get; set; } = string.Empty;
        public byte LoaiDichVu { get; set; }
        public string? GhiChu { get; set; }
    }

    public class UpdateDichVuDTO
    {
        public string TenDichVu { get; set; } = string.Empty;
        public byte LoaiDichVu { get; set; }
        public string? GhiChu { get; set; }
    }
}
