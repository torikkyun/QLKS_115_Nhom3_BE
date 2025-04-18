namespace QLKS_115_Nhom3_BE.DTOs
{
    public class NhanVienDTO
    {
        public int MaNhanVien { get; set; }
        public string Ho { get; set; } = null!;

        public string Ten { get; set; } = null!;

        public string? Email { get; set; }

        public string Sdt { get; set; } = null!;

        public string? Cccd { get; set; }

        public int VaiTro { get; set; }
    }
    public class UpdateNhanVienDTO
    {
        public string Ho { get; set; } = null!;

        public string Ten { get; set; } = null!;

        public string? Email { get; set; }

        public string Sdt { get; set; } = null!;

        public string? Cccd { get; set; }

        public int? VaiTro { get; set; }
    }
}
