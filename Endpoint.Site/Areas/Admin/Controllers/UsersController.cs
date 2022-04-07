using Endpoint.Site.Areas.Admin.Models.ViewModels.UsersViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyStore.Application.Services.UserServices.Command.DisableUserService;
using MyStore.Application.Services.UserServices.Command.RegisterService;
using MyStore.Application.Services.UserServices.Query.GetRolesService;
using MyStore.Application.Services.UserServices.Query.GetUsersService;
using MyStore.Common.ResultDto;
using MyStore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Endpoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {

        private readonly IGetUsersService _getUsersService;
        private readonly IDisableUserService _disableUserService;
        private readonly IRegisterService _registerUserService;
        private readonly IGetRolesService _getRolesService;

        public UsersController(IGetUsersService getUsersService,
                               IDisableUserService disableUserService,
                               IRegisterService registerUserService,
                               IGetRolesService getRolesService)
        {
            _getUsersService = getUsersService;
            _disableUserService = disableUserService;
            _registerUserService = registerUserService;
            _getRolesService = getRolesService;
        }
        public IActionResult UsersList(UsersListRequestViewModel request)
        {

            if (request.SearchKey == null)
            {
                request.SearchKey = "";
            }
            return View(_getUsersService.Execute(new GetUsersRequest(request.SearchKey,request.PageIndex,request.ItemsInPage)));
        }
        [HttpPost]
        public IActionResult ToggleUser(string userName)
        {
            return Json(_disableUserService.Execute(userName));
        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            ViewBag.Roles = new SelectList(_getRolesService.Execute().Data);
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(UserCreateViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResultDto(false, "اطلاعات وارد شده معتبر نیست!"));
            }

            return Json(_registerUserService.Execute(new RequestRegisterUserDto() 
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.UserName,
                Role = request.RoleName,
                Password = request.Password,
                AdminPannelRegisteration = true
            }));
        }
    }
}
