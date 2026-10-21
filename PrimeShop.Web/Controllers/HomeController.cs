using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Wishlist.Dtos;
using PrimeShop.Web.Interface;
using PrimeShop.Web.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PrimeShop.Web.Controllers
{
    public class HomeController : Controller
    {


        private readonly IPrimeShopApi _api;

        public HomeController(IPrimeShopApi api)
        {
            _api = api;
        }


        public async Task<IActionResult> Index()
        {
            var wishlistProductIds = new List<WishlistItemDto>();
            var token = Request.Cookies["jwt"];

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();

                if (handler.CanReadToken(token))
                {
                    var jwtToken = handler.ReadJwtToken(token);

                    if (jwtToken.ValidTo > DateTime.UtcNow)
                    {
                        var roleClaim = jwtToken.Claims
                            .FirstOrDefault(c =>
                                c.Type == ClaimTypes.Role || c.Type == "role")
                            ?.Value;

                        if (roleClaim == "Admin")
                            return RedirectToAction("Dashboard", "Admin");

                        var userId = jwtToken.Claims
                            .FirstOrDefault(c => c.Type == "Id")
                            ?.Value;

                        if (!string.IsNullOrEmpty(userId))
                        {
                            wishlistProductIds =
                                await _api.GetWishlistProductIdsByUserIdAsync(userId);
                        }
                    }
                }
            }

            ViewBag.WishlistProductIds = wishlistProductIds;

            var latestProducts = await _api.GetRandomProductBAsync(8);
            return View(latestProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
