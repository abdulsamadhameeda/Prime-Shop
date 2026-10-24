using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> FilterProducts(int draw, int start, int length, Guid? categoryId, decimal? minPrice, decimal? maxPrice, string? sortBy)
        {
            var query = new GetShopProductsQuery
            {
                Draw = draw,
                Start = start,
                Length = length,
                CategoryId = categoryId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                SortBy = sortBy
            };

            var result = await _api.GetShopProductsAsync(query);

            return Json(result);
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
                Columns = new List<string> { "ProductImage", "Name", "Description", "Price", "StockQuantity", "Category" }
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
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductCommand command, IFormFile? image, [FromServices] IWebHostEnvironment env)
        {
            if (image != null && image.Length > 0)
            {
                var folder = Path.Combine(env.WebRootPath, "images", "products");
                Directory.CreateDirectory(folder);

                var fileName = $"{Guid.NewGuid()}.png";
                using (var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                command = command with { ImageUrl = $"/images/products/{fileName}" };
            }

            var result = await _api.UpdateProductBAsync(command);

            if (result != Guid.Empty)
                return Json(new { success = true });

            return BadRequest("error");
        }
    }
}
