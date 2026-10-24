using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Contact.Dtos;
using PrimeShop.Web.Interface;
using Refit;

namespace PrimeShop.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IPrimeShopApi _api;

        public ContactController(IPrimeShopApi api)
        {
            _api = api;
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(
           CreateContactMessageCommand command)
        {
            try
            {
                await _api.CreateContactMessageAsync(command);
                return Ok(new { success = true, message = "Your message has been sent successfully." });
            }
            catch (ApiException)
            {
                return BadRequest(new { success = false, message = "Could not send your message. Please try again." });
            }
        }







    }
}
