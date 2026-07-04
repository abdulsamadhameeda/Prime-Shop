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
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;

            return View(products);
        }





        [HttpGet]
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            var product = await _api.GetProductByIdAsync(id);
            return View(product);
        }
    }
}
