using Endpoint.Site.Helpers.Filters;
using Endpoint.Site.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyStore.Application.Services.VisitorServices.SaveVisitorInfoService;
using MyStore.Infrastructure.IdentityConfigs;
using MyStore.Infrastructure.StartupConfigureServices.AutoMapperServicesConfigs;
using MyStore.Infrastructure.StartupConfigureServices.ProductServicesConfigs;
using MyStore.Infrastructure.StartupConfigureServices.VisitorServicesConfigs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Endpoint.Site
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
            //////////////////////////////////////////////////////////////////////
            //asp.net core built-in services and Filters
            //////////////////////////////////////////////////////////////////////
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
            services.AddAuthorization();
            services.AddSignalR();

            services.AddScoped<GetVisitorInfoActionFilter>();
            services.AddControllersWithViews()
                .AddMvcOptions(options =>
                {
                    options.Filters.Add<GetVisitorInfoActionFilter>();
                });
            //////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////

            services.AddIdentityConfigs(Configuration);
            services.AddAutoMapperServices();
            services.AddVisitorServices();
            services.AddUtilityServices();
            services.AddUserServices();
            services.AddProductServices();
            services.AddInfrastructureServices();

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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                  );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<OnlineUsersHub>("/chathub");

            });
        }
    }
}
