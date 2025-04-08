using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLKS_115_Nhom3_BE.DTOs;
using QLKS_115_Nhom3_BE.Helpers;
using QLKS_115_Nhom3_BE.Services;

namespace QLKS_115_Nhom3_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly JwtHelper _jwtHelper;

        public AuthController(IAuthService authService, JwtHelper jwtHelper)
        {
            _authService = authService;
            _jwtHelper = jwtHelper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            // Validate MaVaiTro
            if (registerDto.MaVaiTro != 1 && registerDto.MaVaiTro != 2)
            {
                return BadRequest("VaiTro không hợp lệ");
            }

            var result = await _authService.Register(registerDto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var user = await _authService.Login(loginDto);

            if (user == null)
            {
                return Unauthorized("Email hoặc mật khẩu không đúng");
            }

            var token = _jwtHelper.GenerateToken(user.Email, user.VaiTro);

            return Ok(new
            {
                Token = token,
                User = user
            });
        }
    }
}
