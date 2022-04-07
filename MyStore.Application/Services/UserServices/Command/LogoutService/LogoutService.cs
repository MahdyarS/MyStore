using Microsoft.AspNetCore.Identity;
using MyStore.Domain.Entities.User;

namespace MyStore.Application.Services.UserServices.Command.LogoutService
{
    public class LogoutService : ILogoutService
    {
        private readonly SignInManager<User> _signInManager;

        public LogoutService(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public void Execute()
        {
            _signInManager.SignOutAsync().Wait();
        }
    }
}
