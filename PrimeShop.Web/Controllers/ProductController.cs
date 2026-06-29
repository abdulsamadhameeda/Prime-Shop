using Microsoft.AspNetCore.Mvc;
using PrimeShop.Web.Interface;

namespace PrimeShop.Web.Controllers
{
    public class ProductController : Controller
    {


        private readonly IPrimeShopApi _api;

        public ProductController(IPrimeShopApi api)
        {
            _api = api;
        }

        [HttpGet]
        public async Task<IActionResult> ShopCategory(Guid? categoryId , decimal? minPrice , decimal? maxPrice )
        {
            var products = await _api.GetProductsAsync(categoryId, minPrice, maxPrice);
            var categories = await _api.GetCategoriesAsync();

            ViewBag.Categories = categories;


            return View(products);
        }

        [HttpGet]
        public IActionResult ProductDetails()
        {
            return View();
        }
    }
}
