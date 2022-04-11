using Microsoft.Extensions.DependencyInjection;
using MyStore.Application.Interfaces.ContextsAndRepositories;
using MyStore.Application.Services.ProductsServices.Command.AddNewCategoryService;
using MyStore.Application.Services.ProductsServices.Query.GetAllSubCategoriesService;
using MyStore.Application.Services.ProductsServices.Query.GetCategoryById;
using MyStore.Persistence.ContextsAndRepositories.CategoriesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Infrastructure.StartupConfigureServices.ProductServicesConfigs
{
    public static class ProductServices
    {
        public static IServiceCollection AddProductServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IGetAllSubCategoriesService, GetAllSubCategoriesService>();
            services.AddScoped<IAddNewCategoryService, AddNewCategoryService>();
            services.AddScoped<ICategoriesRepositoryInterface, CategoriesRepository>();
            services.AddScoped<IGetCategoryById, GetCategoryById>();


            return services;
        }
    }
}
