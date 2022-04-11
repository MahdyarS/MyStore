using Microsoft.AspNetCore.Mvc;
using MyStore.Application.Services.ProductsServices.Query.GetAllSubCategoriesService;
using MyStore.Application.Services.ProductsServices.Query.GetCategoryById;
using System.Threading.Tasks;

namespace Endpoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IGetAllSubCategoriesService _getAllSubCategoriesService;
        private readonly IGetCategoryById _getCategoryById;
        public ProductsController(IGetAllSubCategoriesService getAllSubCategoriesService,
                                  IGetCategoryById getCategoryById)
        {
            _getAllSubCategoriesService = getAllSubCategoriesService;
            _getCategoryById = getCategoryById;
        }

        [Route("[Area]/[Controller]/[Action]/{CategoryId?}")]
        public async Task<IActionResult> Categories(int CategoryId = 0)
        {
            ViewBag.parent = await _getCategoryById.Execute(CategoryId);
            return View(await _getAllSubCategoriesService.Execute(CategoryId));
        }
    }
}
