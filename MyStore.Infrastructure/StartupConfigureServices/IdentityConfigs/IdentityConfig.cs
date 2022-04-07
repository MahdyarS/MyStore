using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyStore.Application.Contexts;
using MyStore.Domain.Entities.User;
using MyStore.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Infrastructure.IdentityConfigs
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfigs(this IServiceCollection services,IConfiguration Configuration)
        {
            services.AddDbContext<UsersContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddScoped<IUsersDbContext, UsersContext>();

            services.AddIdentity<User, Role>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            })
            .AddEntityFrameworkStores<UsersContext>()
            .AddDefaultTokenProviders()
            .AddRoles<Role>()
            .AddErrorDescriber<IdentityCustomErrors>();


            //services.Configure<IdentityOptions>(options =>
            //{
            //    //options.Password.RequireDigit = false;
            //    //options.Password.RequiredLength = 8;
            //    //options.Password.RequireLowercase = false;
            //    //options.Password.RequireNonAlphanumeric = false;
            //    //options.Password.RequireUppercase = false;
            //    //options.Password.RequiredUniqueChars = 1;
            //    //options.Password.RequireNonAlphanumeric = false;
            //    //options.User.RequireUniqueEmail = true;
            //    //options.Lockout.MaxFailedAccessAttempts = 5;
            //    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            //});

            services.Configure<IdentityOptions>(options =>
            {
                //User configurations
                options.User.RequireUniqueEmail = true;


                //Password configurations
                options.Password.RequireDigit = true;


                //Lockout configurations
                options.Lockout.MaxFailedAccessAttempts = 12;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(12);


                //SignIn configurations
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;


                //ClaimsIdentity configurations
                options.ClaimsIdentity.EmailClaimType = "";


                //Stores configurations
                //options.Stores.ProtectPersonalData = true;


                //Tokens configurations
                options.Tokens.EmailConfirmationTokenProvider = "";
            });

            services.ConfigureApplicationCookie(option =>
            {
                option.ExpireTimeSpan = TimeSpan.FromDays(365);
                option.LoginPath = "/login";
            });


            return services;
        }
    }
}
