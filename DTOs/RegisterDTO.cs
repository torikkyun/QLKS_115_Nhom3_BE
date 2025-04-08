using System.ComponentModel.DataAnnotations;

namespace QLKS_115_Nhom3_BE.DTOs
{
    public class RegisterDTO
    {
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public string CCCD { get; set; }
        public string MatKhau { get; set; }
        public int MaVaiTro { get; set; } // 1: Quản lý, 2: Lễ tân
    }
}
