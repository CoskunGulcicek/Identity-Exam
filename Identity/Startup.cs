using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Contexts;
using Identity.CustomValidator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Identity
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UdemyContext>();
            services.AddIdentity<AppUser, AppRole>(
                r =>
                {
                    //aþaðýdaki kontrolleri þifreyi 1 veye basit bir þey verip hýzlýca giriþ yapmak için ekledim test için aktif edebilirsiniz
                    //r.Password.RequireDigit = false;
                    //r.Password.RequireLowercase = false;
                    //r.Password.RequiredLength = 1;
                    //r.Password.RequireNonAlphanumeric = false;
                    //r.Password.RequireUppercase = false;

                    //r.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                    //r.Lockout.MaxFailedAccessAttempts = 5;

                    //r.SignIn.RequireConfirmedEmail = true;
                }
                ).AddErrorDescriber<CustomIdentityValidator>().AddPasswordValidator<CustomPasswordValidator>()
                .AddEntityFrameworkStores<UdemyContext>();

            services.ConfigureApplicationCookie(r =>
            {
                r.LoginPath = new PathString("/Home/Index");
                r.Cookie.HttpOnly = true;
                r.AccessDeniedPath = new PathString("/Home/AccessDenied");
                r.Cookie.Name = "IdentityCookie";
                r.Cookie.SameSite = SameSiteMode.Strict;
                r.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                r.ExpireTimeSpan = TimeSpan.FromDays(30);

            });
            services.AddAuthorization(x=> {
                x.AddPolicy("FemalePolicy", cnf => { cnf.RequireClaim("gender", "female"); });
            });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
