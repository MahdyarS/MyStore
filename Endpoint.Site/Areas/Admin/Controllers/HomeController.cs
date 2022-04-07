using Microsoft.AspNetCore.Mvc;
using MyStore.Application.Services.UtilityServices.MultiServicePageServices.AdminHomePageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Endpoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IAdminHomePageService _adminHomePageService;

        public HomeController(IAdminHomePageService adminHomePageService)
        {
            _adminHomePageService = adminHomePageService;
        }

        public IActionResult Index()
        {
            return View(_adminHomePageService.Execute());
        }
    }
}
