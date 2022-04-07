using Microsoft.Extensions.DependencyInjection;
using MyStore.Application.Services.UserServices.Command.DisableUserService;
using MyStore.Application.Services.UserServices.Command.EmailConfirmationService.ConfirmEmailService;
using MyStore.Application.Services.UserServices.Command.EmailConfirmationService.SendConfirmationEmailService;
using MyStore.Application.Services.UserServices.Command.LoginService;
using MyStore.Application.Services.UserServices.Command.LogoutService;
using MyStore.Application.Services.UserServices.Command.RegisterService;
using MyStore.Application.Services.UserServices.Command.ResetPasswordServices;
using MyStore.Application.Services.UserServices.Command.ResetPasswordServices.ResetPasswordService;
using MyStore.Application.Services.UserServices.Query.GetRolesService;
using MyStore.Application.Services.UserServices.Query.GetUsersService;
using MyStore.Application.Services.UtilityServices.UrlGeneratorService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UserServices
    {
        public static IServiceCollection AddUserServices(this IServiceCollection services)
        {
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILogoutService, LogoutService>();
            services.AddScoped<IGetUsersService, GetUsersService>();
            services.AddScoped<IDisableUserService, DisableUserService>();
            services.AddScoped<IGetRolesService, GetRolesService>();
            services.AddScoped<ISendConfirmationEmailService, SendConfirmationEmailService>();
            services.AddScoped<IConfirmEmailService, ConfirmEmailService>();
            services.AddScoped<IUrlGeneratorService, UrlGeneratorService>();
            services.AddScoped<ISendPasswordRecoveryEmailService, SendPasswordRecoveryEmailService>();
            services.AddScoped<IResetPasswordService, ResetPasswordService>();


            return services;
        }
    }
}
