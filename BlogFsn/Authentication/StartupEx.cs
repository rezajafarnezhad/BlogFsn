using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

namespace BlogFsn.Authentication
{
    public static class StartupEx
    {
        public static IApplicationBuilder RedirectStatusCode(this IApplicationBuilder app)
        {
            return app.Use(async (Context, next) =>
            {
                await next.Invoke();

                if (Context.Response.StatusCode == 401)
                {
                    Context.Response.Redirect("/401");
                } 
                if (Context.Response.StatusCode == 403)
                {
                    Context.Response.Redirect("/403");
                } 
                if (Context.Response.StatusCode == 404)
                {
                    Context.Response.Redirect("/404");
                } 
                if (Context.Response.StatusCode == 500)
                {
                    Context.Response.Redirect("/500");
                }

            });
        }
    }
}