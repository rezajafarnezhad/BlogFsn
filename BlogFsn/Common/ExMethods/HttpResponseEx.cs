using System;
using Framework.Application.Consts;
using Microsoft.AspNetCore.Http;

namespace BlogFsn.Common.ExMethods
{
    public static class HttpResponseEx
    {
        public static void CreateAuthCookie(this HttpResponse response, string AuthToken, bool RemmemberMe)
        {
            // حذف کوکی
            response.Cookies.Delete(AuthConst.CookieName);

            // ایجاد کوکی
            response.Cookies.Append(AuthConst.CookieName, AuthToken, RemmemberMe ? new CookieOptions() { Expires = DateTime.Now.AddHours(50) } : new CookieOptions());
        }

    }
}