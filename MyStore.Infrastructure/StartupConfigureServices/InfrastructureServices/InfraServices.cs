using Microsoft.Extensions.DependencyInjection;
using MyStore.Application.Interfaces.SendMessege;
using MyStore.Infrastructure.InfraServices.MessegeSendServices;
using MyStore.Infrastructure.InfraServices.MessegeSendServices.SendEmailService;
using MyStore.Infrastructure.InfraServices.MessegeSendServices.SendSmsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfraServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ISendMessegeInterface, SendGmail>();
            services.AddScoped<ISendMessegeService, SendGmail>();
            services.AddScoped<ISendSmsService, SendSmsService>();


            return services;
        }
    }
}
