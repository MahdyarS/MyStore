using Microsoft.AspNetCore.Identity;
using MyStore.Common.ResultDto;
using MyStore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.UserServices.Command.ResetPasswordServices.ResetPasswordService
{
    public interface IResetPasswordService
    {
        ResultDto Execute(ResetPasswordDto request);
    }

    public class ResetPasswordService : IResetPasswordService
    {
        private readonly UserManager<User> _userManager;

        public ResetPasswordService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public ResultDto Execute(ResetPasswordDto request)
        {
            User user = _userManager.FindByNameAsync(request.Email).Result;
            if (user == null)
                return new ResultDto(false, "درخواست شما قابل اجرا نیست!");
            var result = _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword).Result;
            if(result.Succeeded)
                return new ResultDto(true, "کلمه عبور با موفقیت تغییر یافت!");
            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += (item.Description + Environment.NewLine);
            }
            return new ResultDto(false, message);
        }
    }

    public class ResetPasswordDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
