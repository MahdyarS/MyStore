using Microsoft.AspNetCore.Identity;
using MyStore.Application.Interfaces.SendMessege;
using MyStore.Application.Services.UtilityServices.UrlGeneratorService;
using MyStore.Common.InfraStructureDtos;
using MyStore.Common.ResultDto;
using MyStore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.UserServices.Command.ResetPasswordServices
{
    public interface ISendPasswordRecoveryEmailService
    {
        ResultDto Execute(string email);
    }
    public class SendPasswordRecoveryEmailService : ISendPasswordRecoveryEmailService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUrlGeneratorService _urlGeneratorService;
        private readonly ISendMessegeInterface _sendMessegeInterface;


        public SendPasswordRecoveryEmailService(UserManager<User> userManager,
                                                IUrlGeneratorService urlGeneratorService,
                                                ISendMessegeInterface sendMessegeInterface)
        {
            _userManager = userManager;
            _urlGeneratorService = urlGeneratorService;
            _sendMessegeInterface = sendMessegeInterface;
        }

        public ResultDto Execute(string email)
        {
            User user = _userManager.FindByNameAsync(email).Result;
            if (user == null)
                return new ResultDto(false, "کاربری با این ایمیل در سیستم ثبت نشده است!");
            if (!_userManager.IsEmailConfirmedAsync(user).Result)
                return new ResultDto(false, "ابتدا ایمیل خود را تایید کنید!");

            var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
            var url = _urlGeneratorService.CreateUrl("ResetPassword", "Account", new { token = token, email = email },null);

            var message = new MessageDto()
            {
                From = "alirezayi2342@gmail.com",
                To = email,
                Subject = "تغییر رمز عبور",
                Body = $"جهت تغییر رمز خود بر روی لینک زیر کلیک کنید!<br><a href ={url}>Link</a>"
            };

            _sendMessegeInterface.SendMessage(message);


            return new ResultDto(true,"ایمیل تغییر رمز عبور با موفقیت برای شما ارسال شد");
        }
    }
}
