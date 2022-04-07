using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MyStore.Application.Interfaces.Contexts;
using MyStore.Application.Services.VisitorServices.GenerateGeneralVisitorsReportService;
using MyStore.Application.Services.VisitorServices.GetLastMonthVisitorsReportService;
using MyStore.Application.Services.VisitorServices.GetTodayVisitsReportService;
using MyStore.Application.Services.VisitorServices.GetTotalVisitorsReportService;
using MyStore.Application.Services.VisitorServices.OnlineVisitorsService;
using MyStore.Application.Services.VisitorServices.SaveVisitorInfoService;
using MyStore.Persistence.Contexts.MongoDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Infrastructure.StartupConfigureServices.VisitorServicesConfigs
{
    public static class VisitorServices
    {
        public static IServiceCollection AddVisitorServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IMongoDbContext<>), typeof(MongoDbContext<>));
            services.AddTransient<IMongoClient, MongoClient>();
            services.AddTransient<ISaveVisitorsInfoService, SaveVisitorsInfoService>();
            services.AddTransient<IGetLastMonthVisitorsReportService, GetLastMonthVisitorsReportService>();
            services.AddTransient<IGetTodayVisitsReportService, GetTodayVisitsReportService>();
            services.AddTransient<IGetTotalVisitorsReportService, GetTotalVisitorsReportService>();
            services.AddTransient<IGenerateGeneralVisitorsReportService, GenerateGeneralVisitorsReportService>();
            services.AddTransient<IOnlineVisitorService, OnlineVisitorService>();

            return services;
        }
    }
}
