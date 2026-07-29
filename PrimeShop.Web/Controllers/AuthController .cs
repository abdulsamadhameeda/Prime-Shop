using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Auth.Login.Dtos;
using PrimeShop.Application.features.Auth.Register.Dtos;
using PrimeShop.Web.Interface;
using Refit;
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
            try
            {
                var token = await _api.LoginAsync(command);

                HttpContext.Response.Cookies.Append("jwt", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays(1)
                });
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                var role = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role")?.Value;

                if (role == "Admin")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password");
                return View(command);
            }
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            try
            {
                await _api.RegisterAsync(command);

                return RedirectToAction("Login");
            }

            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var validationErrors = await ex.GetContentAsAsync<ValidationProblemDetails>();

                    if (validationErrors?.Errors != null)
                    {
                        foreach (var error in validationErrors.Errors)
                        {
                            var errorMessage = string.Join(" | ", error.Value);
                            ModelState.AddModelError(error.Key, errorMessage);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An unexpected error occurred, please try again later.");
                }

                return View(command);
            }

        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("jwt");
            return RedirectToAction("Login");
        }

    }
}

