using Microsoft.AspNetCore.Mvc;

namespace PrimeShop.Web.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
