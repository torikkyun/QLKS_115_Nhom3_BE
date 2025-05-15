using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLKS_115_Nhom3_BE.Helpers;
using QLKS_115_Nhom3_BE.Models;
using QLKS_115_Nhom3_BE.DTOs;

namespace QLKS_115_Nhom3_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoaiDichVuController : ControllerBase
    {
        private readonly IDbConnection _db;
        public LoaiDichVuController(IDbConnection db)
        {
            _db = db;
        }

        // GET: api/LoaiDichVu
        [HttpGet]
        public async Task<ActionResult<PagedResult<LoaiDichVuDTO>>> Get(int page = 1, int pageSize = 10)
        {
            var sql = @"SELECT * FROM LoaiDichVuEnum";
            var result = await PaginationHelper.GetPagedDataAsync<LoaiDichVuDTO>(_db, sql, page, pageSize);
            return Ok(result);
        }

        // GET api/LoaiDichVu/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LoaiDichVuDTO>> Get(int id)
        {
            var result = await _db.QueryFirstOrDefaultAsync<LoaiDichVuDTO>(
                @"SELECT * FROM LoaiDichVuEnum
                WHERE Id = @Id",
                new { Id = id });

            return result == null ? NotFound() : Ok(result);
        }
    }
}
