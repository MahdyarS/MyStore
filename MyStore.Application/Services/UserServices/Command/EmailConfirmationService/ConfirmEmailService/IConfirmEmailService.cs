using Microsoft.AspNetCore.Identity;
using MyStore.Common.ResultDto;
using MyStore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.UserServices.Command.EmailConfirmationService.ConfirmEmailService
{
    public interface IConfirmEmailService
    {
        ResultDto Execute(ConfirmEmailRequestDto request);
    }
    public class ConfirmEmailService : IConfirmEmailService
    {
        private readonly UserManager<User> _userManager;

        public ConfirmEmailService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public ResultDto Execute(ConfirmEmailRequestDto request)
        {
            var user = _userManager.FindByNameAsync(request.UserName).Result;
            if (request.UserName == null || request.Token == null || user == null)
                return new ResultDto(false,"اطلاعات نادرست!");

            var result = _userManager.ConfirmEmailAsync(user, request.Token).Result;

            if (result.Succeeded)
                return new ResultDto(true,"حساب کاربری شما با موفقیت فعال شد!");

            else
                return new ResultDto(false, "حساب کاربری فعال نشد!");
        }
    }

    public class ConfirmEmailRequestDto
    {
        public ConfirmEmailRequestDto(string userName, string token)
        {
            UserName = userName;
            Token = token;
        }

        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
