using Microsoft.EntityFrameworkCore;
using QLKS_115_Nhom3_BE.Models;
using System.Security.Claims;

namespace QLKS_115_Nhom3_BE
{
    public class CurrentUserMiddleware
    {
        private readonly RequestDelegate _next;

        public CurrentUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, DataQlks115Nhom3Context dbContext)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var email = context.User.FindFirstValue(ClaimTypes.Email);
                var nhanVien = await dbContext.NhanViens.FirstOrDefaultAsync(x => x.Email == email);

                if (nhanVien != null)
                {
                    context.Items["CurrentNhanVien"] = nhanVien;
                }
            }

            await _next(context);
        }
    }
}
