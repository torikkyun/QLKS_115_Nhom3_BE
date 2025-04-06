using Dapper;
using QLKS_115_Nhom3_BE.Models;
using System.Data;

namespace QLKS_115_Nhom3_BE.Helpers
{
    public static class PaginationHelper
    {
        public static async Task<PagedResult<T>> GetPagedDataAsync<T>(
            IDbConnection db, string baseQuery, int page, int pageSize, object? parameters = null)
        {
            var offset = (page - 1) * pageSize;

            var pagedQuery = $"{baseQuery} ORDER BY (SELECT NULL) OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            var countQuery = $"SELECT COUNT(*) FROM ({baseQuery}) AS CountTable";

            var data = await db.QueryAsync<T>(pagedQuery, new { Offset = offset, PageSize = pageSize, parameters });

            var totalRecords = await db.ExecuteScalarAsync<int>(countQuery, parameters);

            return new PagedResult<T>
            {
                TotalRecords = totalRecords,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                Data = data.ToList()
            };
        }
    }
}