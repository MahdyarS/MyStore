using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using MyStore.Application.Interfaces.SendMessege;
using MyStore.Application.Services.UtilityServices.UrlGeneratorService;
using MyStore.Common.InfraStructureDtos;
using MyStore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.UserServices.Command.EmailConfirmationService.SendConfirmationEmailService
{
    public interface ISendConfirmationEmailService
    {
        void Execute(User user);
    }
    public class SendConfirmationEmailService : ISendConfirmationEmailService
    {
        private ISendMessegeInterface _sendMessegeInterface;
        private UserManager<User> _userManager;
        private IUrlGeneratorService _urlGeneratorService;

        public SendConfirmationEmailService(ISendMessegeInterface sendMessegeInterface,
                                            UserManager<User> userManager,
                                            IUrlGeneratorService urlGeneratorService)
        {
            _sendMessegeInterface = sendMessegeInterface;
            _userManager = userManager;
            _urlGeneratorService = urlGeneratorService;
        }

        public void Execute(User user)
        {
            var token = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;

            var message = new MessageDto()
            {
                From = "alirezayi2342@gmail.com",
                To = user.Email,
                Subject = "تایید ایمیل",
            };
            
            var url = _urlGeneratorService.CreateUrl("ConfirmEmail", "Account",values: new { userName = user.UserName, token = token });
            
            message.Body = $"برای تایید حساب کاربری خود روی لینک زیر کلیک کنید! <br><a href ={url}>Link</a>";

            _sendMessegeInterface.SendMessage(message);

        }
    }
}
