using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Basket.Dtos;
using PrimeShop.Application.features.BasketItem.Dtos;
using PrimeShop.Web.Interface;
using System.IdentityModel.Tokens.Jwt;

namespace PrimeShop.Web.Controllers
{
    //[Authorize(Roles = "Customer")]
    public class BasketController : Controller
    {


        private readonly IPrimeShopApi _api;

        public BasketController(IPrimeShopApi api)
        {
            _api = api;
        }

        [HttpGet]
        public async Task<IActionResult> ShippingCart()
        {

            var token = HttpContext.Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(token)) return View();

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var customerId = Guid.Parse(jwt.Claims.First(c => c.Type == "CustomerId").Value);

            var basket = await _api.GetBasketByCustomerIdAsync(customerId);

            if (basket == null)
            {
                await _api.CreateBasketAsync(new CreateBasketCommand(customerId));
            }




            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddItemToBasket([FromBody] List<BasketItemDto> items)
        {
            var token = HttpContext.Request.Cookies["jwt"];

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception();
            }

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var customerId = Guid.Parse(jwt.Claims.First(c => c.Type == "CustomerId").Value);

            var basket = await _api.GetBasketByCustomerIdAsync(customerId);


            foreach (var item in items)
            {
                var existingItems = await _api.GetBasketItemsByBasketIdAsync(basket.Id);

                var existing = existingItems?.FirstOrDefault(x => x.ProductId == item.ProductId);

                if (existing != null)
                {
                    await _api.UpdateBasketItemAsync(new UpdateBasketItemCommand(existing.Id, item.Quantity + existing.Quantity, existing.BasketId, existing.ProductId));
                }
                else
                {
                    await _api.CreateBasketItemAsync(new CreateBasketItemCommand(item.Quantity, basket.Id, item.ProductId));
                }
            }


            return Json(true);
        }







        [HttpPost]
        public async Task<IActionResult> GetProductsByIds([FromBody] List<Guid> ids)
        {
            var products = await _api.GetProductsByIdsAsync(ids);
            return Json(products);
        }


        [HttpGet]
        public async Task<IActionResult> GetBasketItems()
        {
            var token = HttpContext.Request.Cookies["jwt"];
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var customerId = Guid.Parse(jwt.Claims.First(c => c.Type == "CustomerId").Value);

            var basket = await _api.GetBasketByCustomerIdAsync(customerId);
            if (basket == null) return Json(new List<BasketItemDto>());

            var items = await _api.GetBasketItemsByBasketIdAsync(basket.Id);
            return Json(items);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteItem([FromQuery] Guid id)
        {
            await _api.DeleteBasketItemAsync(id);
            return Ok();
        }
    }
}
