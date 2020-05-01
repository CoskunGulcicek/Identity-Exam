using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Contexts;
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
                    //a�a��daki kontrolleri �ifreyi 1 veye basit bir �ey verip h�zl�ca giri� yapmak i�in ekledim yani kontrolleri esnettim
                    r.Password.RequireDigit = false;
                    r.Password.RequireLowercase = false;
                    r.Password.RequiredLength = 1;
                    r.Password.RequireNonAlphanumeric = false;
                    r.Password.RequireUppercase = false;
                }
                ).AddEntityFrameworkStores<UdemyContext>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
