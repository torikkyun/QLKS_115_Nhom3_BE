namespace QLKS_115_Nhom3_BE.DTOs
{
  
    public class UpdateNhanVienDTO
    {
        public string Ho { get; set; } = null!;

        public string Ten { get; set; } = null!;

        public string? Email { get; set; }

        public string Sdt { get; set; } = null!;

        public string? Cccd { get; set; }

        public int? VaiTro { get; set; }
    }
    public class NhanVienDTO : UpdateNhanVienDTO
    {
        public int MaNhanVien { get; set; }
    }
    public class CreateNhanVienDTO : UpdateNhanVienDTO
    {
        public string MatKhau { get; set; }
    }
}
