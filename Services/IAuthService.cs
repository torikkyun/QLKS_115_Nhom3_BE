using QLKS_115_Nhom3_BE.DTOs;
using QLKS_115_Nhom3_BE.Models;

namespace QLKS_115_Nhom3_BE.Services
{
    public interface IAuthService
    {
        Task<AuthResult> Register(RegisterDTO registerDto);
        Task<NhanVien> Login(LoginDTO loginDto);
    }

    public class AuthResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
