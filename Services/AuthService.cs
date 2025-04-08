using Microsoft.AspNetCore.Identity;
using QLKS_115_Nhom3_BE.DTOs;
using QLKS_115_Nhom3_BE.Helpers;
using QLKS_115_Nhom3_BE.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;


namespace QLKS_115_Nhom3_BE.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataQlks115Nhom3Context _context;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(DataQlks115Nhom3Context context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResult> Register(RegisterDTO registerDto)
        {
            // Kiểm tra email đã tồn tại
            if (await _context.NhanViens.AnyAsync(x => x.Email == registerDto.Email))
            {
                return new AuthResult { Success = false, Message = "Email đã tồn tại" };
            }

            // Mã hóa mật khẩu
            var hashedPassword = _passwordHasher.HashPassword(registerDto.MatKhau);

            var nhanVien = new NhanVien
            {
                Ho = registerDto.Ho,
                Ten = registerDto.Ten,
                Email = registerDto.Email,
                Sdt = registerDto.SDT,
                Cccd = registerDto.CCCD,
                MatKhau = hashedPassword,
                VaiTro = registerDto.MaVaiTro
            };

            _context.NhanViens.Add(nhanVien);
            await _context.SaveChangesAsync();

            return new AuthResult { Success = true, Message = "Đăng ký thành công" };
        }

        public async Task<NhanVien> Login(LoginDTO loginDto)
        {
            var nhanVien = await _context.NhanViens
                .FirstOrDefaultAsync(x => x.Email == loginDto.Email);

            if (nhanVien == null)
            {
                return null;
            }

            var passwordVerified = _passwordHasher.VerifyPassword(nhanVien.MatKhau, loginDto.MatKhau);

            if (!passwordVerified)
            {
                return null;
            }

            return nhanVien;
        }
    }
}
