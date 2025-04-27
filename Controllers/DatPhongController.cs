using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLKS_115_Nhom3_BE.DTOs;
using QLKS_115_Nhom3_BE.Services;
using QLKS_115_Nhom3_BE.Utilities;

namespace QLKS_115_Nhom3_BE.Controllers
{
    [Authorize(Roles = "1")]
    [ApiController]
    [Route("api/[controller]")]
    public class DatPhongController : ControllerBase
    {
        private readonly IDatPhongService _datPhongService;

        public DatPhongController(IDatPhongService datPhongService)
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
    }
}
