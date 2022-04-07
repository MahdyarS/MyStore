using Microsoft.AspNetCore.Identity;
using MyStore.Common.ResultDto;
using MyStore.Domain.Entities.User;

namespace MyStore.Application.Services.UserServices.Command.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public LoginService(SignInManager<User> signInManager,
                            UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public ResultDto Execute(RequestLoginDto request)
        {
            _signInManager.SignOutAsync().Wait();
            var user = _userManager.FindByNameAsync(request.UserName).Result;
            if(user == null)
            {
                return new ResultDto(false, "کاربر مورد نظر یافت نشد!");
            }
            var result = _signInManager.PasswordSignInAsync(request.UserName, request.Password, request.IsPersistent, true).Result;
            if(result.IsLockedOut)
                return new ResultDto(false,"حساب کاربری شما غیرفعال می باشد!");
            if (!result.Succeeded)
                return new ResultDto(false, "رمز وارد شده صحیح نمی باشد!");
            if (result.RequiresTwoFactor == true)
            {
                //
            }
            if (result.IsLockedOut)
            {
                //
            }

            return new ResultDto(true, "ورود با موفقیت انجام شد!");
        }
    }
}
