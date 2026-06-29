using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.BasketItem.Dtos;
using PrimeShop.Web.Interface;

namespace PrimeShop.Web.Controllers
{
    public class BasketController : Controller
    {


        private readonly IPrimeShopApi _api;

        public BasketController(IPrimeShopApi api)
        {
            _api = api;
        }
            
        public IActionResult ShippingCart()
        {
            return View();
        }


       


    }
}
