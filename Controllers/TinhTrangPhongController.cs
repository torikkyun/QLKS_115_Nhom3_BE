using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLKS_115_Nhom3_BE.Helpers;
using QLKS_115_Nhom3_BE.Models;
using QLKS_115_Nhom3_BE.DTOs;
using Dapper;

namespace QLKS_115_Nhom3_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TinhTrangPhongController : ControllerBase
    {
        private readonly IDbConnection _db;
        public TinhTrangPhongController(IDbConnection db)
        {
            _db = db;
        }

        // GET: api/TinhTrangPhong
        [HttpGet]
        public async Task<ActionResult<PagedResult<TinhTrangPhongDTO>>> Get(int page = 1, int pageSize = 10)
        {
            var sql = @"SELECT * FROM TinhTrangPhongEnum";
            var result = await PaginationHelper.GetPagedDataAsync<TinhTrangPhongDTO>(_db, sql, page, pageSize);
            return Ok(result);
        }

        // GET api/TinhTrangPhong/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TinhTrangPhongDTO>> Get(int id)
        {
            var result = await _db.QueryFirstOrDefaultAsync<TinhTrangPhongDTO>(
                @"SELECT * FROM TinhTrangPhongEnum
                WHERE Id = @Id",
                new { Id = id });

            return result == null ? NotFound() : Ok(result);
        }
    }
}
