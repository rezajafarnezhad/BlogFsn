using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using BlogFsn.Authentication;
using BlogFsn.Common.MessageBox;
using Framework.Application;
using Framework.Application.Consts;
using Framework.Domain;
using Framework.Infrastructure;
using Fsn.Infrastracture.Core;
using Microsoft.Extensions.WebEncoders;

namespace BlogFsn
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomIdentity()
                .AddErrorDescriber<CustomIdentityError>();
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("AdminPanelPolicy", pol => pol.RequireRole(new string[] { "AdminPage" }));

            });
            services.AddControllersWithViews();
            services.AddScoped<ImsgBox, msgBox>();
            services.AddScoped<IUploadFile, FileUpload>();
            services.AddScoped<IJWTBuilder, JWTBuilder>();
            services.Config(Configuration.GetConnectionString("FSNConnection"));


            services.AddMvc(options => options.ModelMetadataDetailsProviders.Add(new PersianDataAnnotationsCore.PersianValidationMetadataProvider()));

            services.Configure<WebEncoderOptions>(opt =>
            {
                opt.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.Arabic, UnicodeRanges.BasicLatin);
            });


            

            services.AddJwtAuthentication(AuthConst.SecretCode, AuthConst.SecretKey, AuthConst.Audience, AuthConst.Issuer);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.RedirectStatusCode();
            app.UseRouting();

            app.UseJwtAuthentication(AuthConst.CookieName);
            app.UseMiddleware<NeedToRebuildTokenMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
