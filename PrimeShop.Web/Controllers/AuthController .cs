using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Auth.Login.Dtos;
using PrimeShop.Application.features.Auth.Register.Dtos;
using PrimeShop.Web.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

            //HttpContext.Session.SetString("Token", token);
            HttpContext.Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            });
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var role = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (role == "Admin")
            {
                return RedirectToAction("Dashboard", "Admin"); 
            }

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

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("jwt");
            return RedirectToAction("Login");
        }

    }
}
