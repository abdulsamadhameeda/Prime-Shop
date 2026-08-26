using Microsoft.AspNetCore.Mvc;
using PrimeShop.Web.Interface;

namespace PrimeShop.Web.Controllers
{
    public class CategoryController : Controller
    {


        private readonly IPrimeShopApi _api;

        public CategoryController(IPrimeShopApi api)
        {
            _api = api;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var Categories = await _api.GetCategoriesAsync();

            return Json(Categories);
        }


    }
}
