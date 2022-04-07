using Microsoft.AspNetCore.Identity;
using MyStore.Application.Services.UserServices.Command.EmailConfirmationService.SendConfirmationEmailService;
using MyStore.Common.Enums.RoleNamesEnum;
using MyStore.Common.ResultDto;
using MyStore.Domain.Entities.User;
using System;
using System.Linq;

namespace MyStore.Application.Services.UserServices.Command.RegisterService
{
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ISendConfirmationEmailService _sendConfirmationEmailService;

        public RegisterService(UserManager<User> userManager,
                                 SignInManager<User> signInManager,
                                 RoleManager<Role> roleManager,
                                 ISendConfirmationEmailService sendConfirmationEmailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _sendConfirmationEmailService = sendConfirmationEmailService;
        }

        public ResultDto Execute(RequestRegisterUserDto request)
        {
            if(!request.AdminPannelRegisteration)
                _signInManager.SignOutAsync().Wait();
            var user = new User
            {
                Email = request.Email,
                UserName = request.Email,
                PhoneNumber = request.PhoneNumber,
                FirstName = request.FirstName,
                LastName = request.LastName
            };


            bool roleIsValid = false;
            foreach (var item in Enum.GetValues(typeof(RoleName)))
            {
                if(item.ToString() == request.Role)
                {
                    roleIsValid = true;
                    break;
                }
            }

            if (!roleIsValid)
            {
                return new ResultDto(false, "نقش وارد شده نامعتبر است!");
            }


            var result = _userManager.CreateAsync(user, request.Password).Result;
            var addingRoleResult = _userManager.AddToRoleAsync(user, request.Role).Result;
            if(result.Succeeded && addingRoleResult.Succeeded)
            {
                _sendConfirmationEmailService.Execute(user);
                if (!request.AdminPannelRegisteration)
                    _signInManager.SignInAsync(user, false).Wait();
                return new ResultDto(true, "ثبت نام با موفقیت انجام شد!");
            }

            if (result.Succeeded && !addingRoleResult.Succeeded)
            {
                _userManager.DeleteAsync(user).Wait();
                return new ResultDto(false, "خطایی هنکام عملیات رخ داد!");
            }
            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + "\n";
            }
            return new ResultDto(false, message);

        }
    }
}
