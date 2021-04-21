using BikeRentalService.Business;
using BikeRentalService.Models;
using BikeRentalService.Models.Entities;
using BikeRentalService.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace BikeRentalService
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
            services.AddControllersWithViews();

            services.AddDbContext<BicycleRentalDbContext>(opts =>
            {
                opts.UseSqlServer(Configuration.GetConnectionString("BicycleRentalDbContext"));
            });

            services.AddTransient<IBikeRepository, BikeRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IEmailSender, MailSender>();
            services.AddTransient<ISeedData, SeedData>();

            services.AddIdentity<LoginAccount, UserRole>()
                .AddEntityFrameworkStores<BicycleRentalDbContext>()
                .AddUserStore<UserStore<LoginAccount, UserRole, BicycleRentalDbContext, Guid>>()
                .AddRoleStore<RoleStore<UserRole, BicycleRentalDbContext, Guid>>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddRazorPages();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedData seedData, UserManager<LoginAccount> userManager)
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });

            seedData.SeedDatabase(userManager);
        }
    }
}
