using Microsoft.AspNetCore.Mvc;

namespace PrimeShop.Web.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult Confirmation()
        {
            return View();
        }
        public IActionResult OrderTracking()
        {
            return View();
        }
    }
}
