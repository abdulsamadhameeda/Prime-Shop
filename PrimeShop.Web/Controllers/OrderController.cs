using Microsoft.AspNetCore.Mvc;
using PrimeShop.Domain.Entities;
using PrimeShop.Web.Interface;
using System.IdentityModel.Tokens.Jwt;

namespace PrimeShop.Web.Controllers
{
    public class OrderController : Controller
    {


        private readonly IPrimeShopApi _api;

        public OrderController(IPrimeShopApi api)
        {
            _api = api;
        }


        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult Confirmation()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> OrderTracking()
        {

            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var customerId = Guid.Parse(jwt.Claims.First(c => c.Type == "CustomerId").Value);

            var order = await _api.GetOrderByIdAndCustomerIdAsync(null, customerId);
            if (order == null) {
                return View("OrderNotFound");
            }

            return View(order);
        }



        [HttpPost]
        public async Task<IActionResult> OrderTracking(Guid? orderId)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var customerId = Guid.Parse(jwt.Claims.First(c => c.Type == "CustomerId").Value);

            var order = await _api.GetOrderByIdAndCustomerIdAsync(orderId, customerId);
            if (order == null)
                return View("OrderNotFound");

            return View(order);
        }
    }
}
