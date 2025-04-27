using Dapper;
using System.Data;
using QLKS_115_Nhom3_BE.Utilities;

namespace QLKS_115_Nhom3_BE.Services
{
    public interface IKhachHangService
    {
        Task<int> CreateKhachHangAsync(CreateKhachHangDTO model);
    }

    public class KhachHangService : IKhachHangService
    {
        private readonly IDbConnection _db;

        public KhachHangService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<int> CreateKhachHangAsync(CreateKhachHangDTO model)
        {
            var checkSql = "SELECT COUNT(*) FROM KhachHang WHERE Cccd = @Cccd";
            var exists = await _db.ExecuteScalarAsync<int>(checkSql, new { model.Cccd });

            if (exists > 0)
            {
                throw new ConflictException("CCCD đã tồn tại trong hệ thống."); 
            }

            var sql = @"INSERT INTO KhachHang (Ho, Ten, Email, Sdt, Cccd) 
                    VALUES (@Ho, @Ten, @Email, @Sdt, @Cccd);
                    SELECT CAST(SCOPE_IDENTITY() as int)";

            return await _db.ExecuteScalarAsync<int>(sql, model);
        }
    }
}