using Microsoft.AspNetCore.Mvc;
using PrimeShop.Web.Interface;
using PrimeShop.Web.Models;
using System.Diagnostics;

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
