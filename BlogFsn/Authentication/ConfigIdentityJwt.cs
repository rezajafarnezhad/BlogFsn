using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BlogFsn.Authentication
{
    public static class ConfigIdentityJwt
    {
        private static string _SecretKey;
        private static byte[] _SecrectCode;

        public static void AddJwtAuthentication(this IServiceCollection services, string SecretCode, string SecretKey, string Audience, string Issuer)
        {
            _SecretKey = SecretKey;
            _SecrectCode = Encoding.ASCII.GetBytes(SecretCode);

            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero,
                        RequireSignedTokens = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(_SecrectCode),

                        RequireExpirationTime = true,
                        ValidateLifetime = true,

                        ValidateAudience = true,
                        ValidAudience = Audience,

                        ValidateIssuer = true,
                        ValidIssuer = Issuer
                    };
                });
        }

        public static void UseJwtAuthentication(this IApplicationBuilder app, string CookieName)
        {
            app.UseMiddleware<JwtAuthenticationWebAppMiddleware>(_SecretKey, CookieName);

            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}