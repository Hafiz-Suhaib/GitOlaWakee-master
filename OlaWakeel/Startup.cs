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
using OlaWakeel.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OlaWakeel.Data.ApplicationUser;
using OlaWakeel.Services.DegreeService;
using OlaWakeel.Services.DegreeTypeService;
using OlaWakeel.Services.LicenseCityService;
using OlaWakeel.Services.LicenseDistrictService;
using OlaWakeel.Services.CustomerService;
using OlaWakeel.Services.ISpecializationService.cs;
using OlaWakeel.Services.LawyerService;
using OlaWakeel.Services.CaseCategoryService;
using OlaWakeel.Services.LawyerExperienceService;
using OlaWakeel.Services.LawyerQaulificationService;
using OlaWakeel.Services.LawyerSpecializationService;
using OlaWakeel.Services.LawyerTimingService;

namespace OlaWakeel
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
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.ConfigureApplicationCookie(
                 options => options.LoginPath = "/Auth/Login"
             );
            services.ConfigureApplicationCookie(
               options => options.AccessDeniedPath = "/Access/Denied"
            );

            services.AddControllers();
            services.AddTransient<IDegreeService, DegreeService>();
            services.AddTransient<ILawyerTimingService, LawyerTimingService>();
            services.AddTransient<ILawyerSpecializationService, LawyerSpecializationService>();
            services.AddTransient<ILawyerQaulificationService, LawyerQaulificationService>();
            services.AddTransient<ILawyerExperienceService, LawyerExperienceService>();
            services.AddTransient<ICaseCategoryService, CaseCategoryService>();
            services.AddTransient<ILawyerService, LawyerService>();
            services.AddTransient<ISpecializationService, SpecializationService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ILicenseDistrictService, LicenseDistrictService>();
            services.AddTransient<ILicenseCityService, LicenseCityService>();
            services.AddTransient<IDegreeTypeService, DegreeTypeService>();

        //    services.GetRequiredService<UserManager<IdentityUser>>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Dashboard}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
