using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Order.Dtos;
using PrimeShop.Application.features.Product.Dtos;
using PrimeShop.Web.Interface;
using PrimeShop.Web.Models;

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
        public async Task<IActionResult> ShopCategory()
        {

            var categories = await _api.GetCategoriesAsync();
                
            ViewBag.Categories = categories;
           
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FilterProducts(Guid? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            var products = await _api.GetProductsAsync(categoryId, minPrice, maxPrice);
            return Json(products);
        }



        [HttpGet]
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            var product = await _api.GetProductByIdAsync(id);
            return View(product);
        }



        [HttpGet]
        public async Task<IActionResult> ProductManagment()
        {
            var model = new TableViewModel
            {
                Columns = new List<string> { "ProductImage","Name", "Description", "Price", "StockQuantity", "Category" }
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetProductsData(DataTableRequest request)
        {
            var query = new GetDataTableProductsQuery
            {
                Draw = request.Draw,
                Start = request.Start,
                Length = request.Length,
                Search = request.Search?.Value ?? ""
            };


            var response = await _api.GetProductsDataAsync(query);

            return Json(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdProduct(Guid id)
        {

            var product = await _api.GetProductByIdAsync(id);

            return Json(product);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {

            var result = await _api.UpdateProductBAsync(command);
            if (result != Guid.Empty)
            {
                return Json(new { success = true });
            }

            return BadRequest("error");


        }
    }
}
