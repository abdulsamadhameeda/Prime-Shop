using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Product.Dtos;
using PrimeShop.Application.features.Wishlist.Dtos;
using PrimeShop.Web.Interface;
using System.IdentityModel.Tokens.Jwt;

namespace PrimeShop.Web.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IPrimeShopApi _api;

        public WishlistController(IPrimeShopApi api)
        {
            _api = api;
        }




        [HttpGet]
        public async Task<IActionResult> ProductWishlist()
        {
            var wishlistItems = new List<WishlistItemDto>();
            var products = new List<ProductDto>();

            var token = HttpContext.Request.Cookies["jwt"];

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                var userId = jwt.Claims.First(c => c.Type == "Id").Value;

                wishlistItems =
                    await _api.GetWishlistProductIdsByUserIdAsync(userId);

                if (wishlistItems.Any())
                {
                    var productIds = wishlistItems
                        .Select(item => item.ProductId)
                        .ToList();

                    products = await _api.GetProductsByIdsAsync(productIds);
                }
            }

            ViewBag.WishlistItems = wishlistItems;

            return View(products);
        }





        [HttpGet]
        public async Task<IActionResult> GetWishlistItems()
        {
            var token = HttpContext.Request.Cookies["jwt"];

            if (string.IsNullOrEmpty(token))
                return Json(new List<WishlistItemDto>());

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var userId = jwt.Claims.First(c => c.Type == "Id").Value;

            var items = await _api.GetWishlistProductIdsByUserIdAsync(userId);

            return Json(items);
        }






        [HttpPost]
        public async Task<IActionResult> Add(Guid productId)
        {
            var token = HttpContext.Request.Cookies["jwt"];

            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var userId = jwt.Claims.First(c => c.Type == "Id").Value;

            var id = await _api.AddToWishlistAsync(new AddToWishlistCommand(productId, userId));

            return Ok(id);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid id)
        {
            var token = HttpContext.Request.Cookies["jwt"];

            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var userId = jwt.Claims.First(c => c.Type == "Id").Value;

            var deleted = await _api.DeleteWishlistAsync(
                new DeleteWishlistCommand(id, userId));

            if (!deleted)
                return NotFound();

            return Ok();
        }


    }
}
