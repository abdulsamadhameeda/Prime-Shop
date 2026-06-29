using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Auth.Login.Dtos;
using PrimeShop.Application.features.Auth.Register.Dtos;
using PrimeShop.Web.Interface;
using System.IdentityModel.Tokens.Jwt;

namespace PrimeShop.Web.Controllers
{
    public class AuthController : Controller
    {

        private readonly IPrimeShopApi _api;

        public AuthController(IPrimeShopApi api)
        {
            _api = api;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var token = await _api.LoginAsync(command);

            HttpContext.Session.SetString("Token", token);

            return RedirectToAction("Index", "Home");
        }

      
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            await _api.RegisterAsync(command);

            return RedirectToAction("Login");
        }


      
    }
}
