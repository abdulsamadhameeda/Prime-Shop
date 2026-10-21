using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Order.Dtos;
using PrimeShop.Application.features.OrderItem.Dtos;
using PrimeShop.Application.features.Product.Dtos;
using PrimeShop.Domain.Entities;
using PrimeShop.Domain.Enums;
using PrimeShop.Web.Interface;
using PrimeShop.Web.Models;
using Refit;
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

        [HttpGet]
        public async Task<IActionResult> CheckoutAsync()
        {
            //var token = HttpContext.Session.GetString("Token");
            var token = HttpContext.Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            var cities = await _api.GetSpecificTypeAsync("City");

            ViewBag.Cities = cities;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Checkout(string shippingStreet, Guid ShippingCityId, decimal totalAmount)
        {

            var token = HttpContext.Request.Cookies["jwt"];
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var userId = jwt.Claims.First(c => c.Type == "Id").Value;
            var basket = await _api.GetBasketByCustomerIdAsync(userId);
            var items = await _api.GetBasketItemsByBasketIdAsync(basket.Id);

            var productIds = items.Select(x => x.ProductId).ToList();
            var products = await _api.GetProductsByIdsAsync(productIds);

            foreach (var item in items)
            {
                var product = products.FirstOrDefault(x => x.Id == item.ProductId);

                if (product == null)
                {
                    TempData["Error"] = "One of the products in your cart is no longer available.";
                    return RedirectToAction("ShippingCart", "Basket"); 
                }

                if (item.Quantity > product.StockQuantity)
                {
                    TempData["Error"] = $"Sorry, the quantity for '{product.Name}' exceeds available stock. We only have {product.StockQuantity} left.";
                    return RedirectToAction("ShippingCart", "Basket"); 
                }
            }

            foreach (var item in items)
            {
                var product = products.First(x => x.Id == item.ProductId); 

                var newQuantity = product.StockQuantity - item.Quantity;

                await _api.UpdateProductBAsync(new UpdateProductCommand(
                          product.Id,
                          product.Name,
                          product.Description,
                          product.Price,
                          newQuantity,
                          product.ImageUrl,
                          product.CategoryId
                ));
            }

            var orderId = await _api.CreateOrderAsync(new CreateOrderCommand(
                DateTime.Now,
                totalAmount,
                OrderStatus.Pending,
                shippingStreet,
                ShippingCityId,
                userId
            ));

            foreach (var item in items)
            {
                var product = products.First(x => x.Id == item.ProductId);
                await _api.CreateOrderItemAsync(new CreateOrderItemCommand(item.Quantity, product.Price, orderId, item.ProductId));
                await _api.DeleteBasketItemAsync(item.Id);
            }

            return RedirectToAction("OrderConfirmation", new { orderId = orderId });

        }



        [HttpGet]
        public async Task<IActionResult> OrderConfirmationAsync(Guid orderId)
        {
            var order = await _api.GetOrderByIdAsync(orderId);

            var orderItems = await _api.GetOrderItemAsync(orderId);

            ViewBag.OrderItems = orderItems;


            return View(order);
        }



        [HttpGet]
        public async Task<IActionResult> OrderTracking()
        {

            var token = HttpContext.Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var userId = jwt.Claims.First(c => c.Type == "Id").Value;

            var order = await _api.GetByIdAndUserIdAsync(null, userId);
            if (order == null)
            {
                return View("OrderNotFound");
            }

            return View(order);
        }



        [HttpPost]
        public async Task<IActionResult> OrderTracking(Guid? orderId)
        {
            var token = HttpContext.Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var userId = jwt.Claims.First(c => c.Type == "Id").Value;

            var order = await _api.GetByIdAndUserIdAsync(orderId, userId);
            if (order == null) {
                return View("OrderNotFound");
            }

            return View(order);
        }


        [HttpGet]
        public async Task<IActionResult> OrderManagment()
        {
            var model = new TableViewModel
            {
                Columns = new List<string> { "Id", "Order Date", "Total Amount", "Status", "Shipping Street", "Shipping City" }
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> GetOrdersData(DataTableRequest request)
        {
            var query = new GetDataTableOrdersQuery
            {
                Draw = request.Draw,
                Start = request.Start,
                Length = request.Length,
                Search = request.Search?.Value ??""
            };

          
            var response = await _api.GetOrdersDataAsync(query);

            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdOrder(Guid id)
        {
           
            var order = await _api.GetOrderByIdAsync(id);

            return Json(order); 
        }




        [HttpDelete]
        public async Task<IActionResult> DeleteOrder([FromQuery] Guid id)
        {
            await _api.DeleteOrderAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _api.GetSpecificTypeAsync("City");

            return Json(cities);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
           
                var result = await _api.UpdateOrderAsync(command);
                if (result != Guid.Empty)
                {
                    return Json(new { success = true });
                }

                return BadRequest("error");
            
            
        }
    }
}
