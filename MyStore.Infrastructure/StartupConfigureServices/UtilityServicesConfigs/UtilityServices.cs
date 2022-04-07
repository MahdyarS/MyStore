using Microsoft.Extensions.DependencyInjection;
using MyStore.Application.Services.UtilityServices.MultiServicePageServices.AdminHomePageService;
using MyStore.Application.Services.UtilityServices.UrlGeneratorService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UtilityServices
    {
        public static IServiceCollection AddUtilityServices(this IServiceCollection services)
        {
            services.AddScoped<IUrlGeneratorService, UrlGeneratorService>();
            services.AddScoped<IAdminHomePageService, AdminHomePageService>();

            return services;
        }
    }
}
