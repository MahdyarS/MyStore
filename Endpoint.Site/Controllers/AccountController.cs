using Endpoint.Site.Models.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyStore.Application.Services.UserServices.Command.EmailConfirmationService.ConfirmEmailService;
using MyStore.Application.Services.UserServices.Command.LoginService;
using MyStore.Application.Services.UserServices.Command.LogoutService;
using MyStore.Application.Services.UserServices.Command.RegisterService;
using MyStore.Application.Services.UserServices.Command.ResetPasswordServices;
using MyStore.Application.Services.UserServices.Command.ResetPasswordServices.ResetPasswordService;
using MyStore.Common.ResultDto;
using MyStore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Endpoint.Site.Controllers
{
    public class AccountController : Controller
    {

        private readonly IRegisterService _registerService;
        private readonly ILoginService _loginService;
        private readonly ILogoutService _logoutService;
        private readonly IConfirmEmailService _confirmEmailService;
        private readonly ISendPasswordRecoveryEmailService _sendPasswordRecoveryEmailService;
        private readonly IResetPasswordService _resetPasswordService;

        public AccountController(IRegisterService registerService,
                                 ILoginService loginService,
                                 ILogoutService logoutService,
                                 IConfirmEmailService confirmEmailService,
                                 ISendPasswordRecoveryEmailService sendPasswordRecoveryEmailService,
                                 IResetPasswordService resetPasswordService)
        {
            _registerService = registerService;
            _loginService = loginService;
            _logoutService = logoutService;
            _confirmEmailService = confirmEmailService;
            _sendPasswordRecoveryEmailService = sendPasswordRecoveryEmailService;
            _resetPasswordService = resetPasswordService;
        }


        [HttpGet]
        [Route("[action]")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Register(RegisterViewModel req)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResultDto(false, "اطلاعات وارد شده معتبر نیست!"));
            }
            
            return Json(_registerService.Execute(new RequestRegisterUserDto()
            {
                Email = req.Email,
                FirstName = req.FirstName,
                LastName = req.LastName,
                PhoneNumber = req.PhoneNumber,
                Password = req.Password
            }));
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Login(LoginViewModel req)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResultDto(false, "اطلاعات وارد شده معتبر نیست!"));
            }

            return Json(_loginService.Execute(new RequestLoginDto(req.UserName, req.Password, req.IsPersistent)));
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult Logout()
        {
            _logoutService.Execute();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ConfirmEmail(EmailTokenModel request)
        {
            var result = _confirmEmailService.Execute(new ConfirmEmailRequestDto(request.Email, request.Token));
            if (!result.Succeeded)
                return BadRequest();
            return View(result);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult ForgotPassword(string email)
        {
            return View("ForgotPasswordRequestResult", _sendPasswordRecoveryEmailService.Execute(email));
        }


        [HttpGet]
        [Route("[action]")]
        public IActionResult ResetPassword(EmailTokenModel request)
        {
            return View(new EmailTokenModel { Token = request.Token , Email = request.Email });
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult ResetPassword(ResetPasswordViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResultDto(false, "اطلاعات وارد شده معتبر نیست!"));
            }
            return Json(_resetPasswordService.Execute(new ResetPasswordDto 
            {
                Email = request.Email,
                Token = request.Token,
                NewPassword = request.NewPassword
            }));
        }

    }
}



