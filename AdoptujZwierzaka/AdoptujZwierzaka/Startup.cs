using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using AdoptujZwierzaka.Data;
using AdoptujZwierzaka.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AdoptujZwierzaka
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddTransient<IPetRepository, EFPetRepository>();
            //services.Configure<>
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                /* endpoints.MapControllerRoute(
                    name: "null",
                    pattern: "{category}/Strona{petPage:int}",
                    defaults: new { controller = "Pet", action = "List" });
                endpoints.MapRazorPages(); */
                /* endpoints.MapControllerRoute(
                     name: "null",
                     pattern: "Strona{petPage:int}",
                     defaults: new { controller = "Pet", action = "List", petPage = 1 });
                 endpoints.MapRazorPages(); */
                /* endpoints.MapControllerRoute(
                     name: "null",
                     pattern: "{category}",
                     defaults: new { controller = "Pet", action = "List", petPage = 1 });
                 endpoints.MapRazorPages(); */
                endpoints.MapControllerRoute(
                    name: "null",
                    pattern: "Account/Edit/{petId:int}",
                    defaults: new { controller = "Account", action = "Edit", petId = 1 });
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "null",
                    pattern: "/List{petPage:int}",
                    defaults: new { controller = "Pet", action = "List", petPage = 1 });
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "null",
                    pattern: "",
                    defaults: new { controller = "Pet", action = "List" });
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "null",
                    pattern: "{controller}/{action}/{id?}");
                endpoints.MapRazorPages();
            });
            SeedData.EnsurePetsOperation(app);
        }
    }
}
