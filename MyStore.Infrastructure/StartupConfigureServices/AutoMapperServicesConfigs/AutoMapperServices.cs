using Microsoft.Extensions.DependencyInjection;
using MyStore.Infrastructure.AutoMapperConfigurations.ProductsProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Infrastructure.StartupConfigureServices.AutoMapperServicesConfigs
{
    public static class AutoMapperServices
    {
        public static IServiceCollection AddAutoMapperServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductsMappingProfile));

            return services;
        }
    }
}
