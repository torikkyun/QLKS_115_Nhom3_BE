using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLKS_115_Nhom3_BE.DTOs;
using QLKS_115_Nhom3_BE.Services;

namespace QLKS_115_Nhom3_BE.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DatTraPhongController : ControllerBase
    {
        private readonly IDatPhongService _datPhongService;

        public DatTraPhongController(IDatPhongService datPhongService)
        {
            _datPhongService = datPhongService;
        }

        [HttpPost("dat-phong")]
        public async Task<IActionResult> DatPhong([FromBody] DatPhongRequestDTO request)
        {
            try
            {
                var bookingId = await _datPhongService.DatPhongAsync(request);
                return Ok(new { MaDatPhong = bookingId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("tra-phong")]
        public async Task<IActionResult> TraPhong([FromBody] TraPhongRequestDTO request)
        {
            try
            {
                var result = await _datPhongService.TraPhongAsync(request);
                if (result)
                    return Ok(new { Message = "Phòng đã được trả thành công." });

                return BadRequest(new { Message = "Hóa đơn chưa được thanh toán hoặc không hợp lệ." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi trả phòng.", Details = ex.Message });
            }
        }
    }
}