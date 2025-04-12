namespace QLKS_115_Nhom3_BE.DTOs
{
    public class DichVuDTO
    {
        public int MaDichVu { get; set; }
        public string TenDichVu { get; set; } = null!;
        public enum LoaiDichVu { get, set }
        public string? GhiChu { get; set; }
    }
}