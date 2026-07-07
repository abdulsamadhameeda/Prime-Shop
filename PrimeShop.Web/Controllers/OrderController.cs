using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Order.Dtos;
using PrimeShop.Application.features.OrderItem.Dtos;
using PrimeShop.Application.features.Product.Dtos;
using PrimeShop.Domain.Entities;
using PrimeShop.Domain.Enums;
using PrimeShop.Web.Interface;
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
        public async Task<IActionResult> Checkout(string shippingStreet, string shippingCity, decimal totalAmount)
        {

            //var token = HttpContext.Session.GetString("Token");
            var token = HttpContext.Request.Cookies["jwt"];
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var customerId = Guid.Parse(jwt.Claims.First(c => c.Type == "CustomerId").Value);

            var orderId = await _api.CreateOrderAsync(new CreateOrderCommand(
                DateTime.Now,
                totalAmount,
                OrderStatus.Pending,
                shippingStreet,
                shippingCity,
                customerId
            ));

            var basket = await _api.GetBasketByCustomerIdAsync(customerId);
            var items = await _api.GetBasketItemsByBasketIdAsync(basket.Id);

            var productIds = items.Select(x => x.ProductId).ToList();
            var products = await _api.GetProductsByIdsAsync(productIds);

            foreach (var item in items)
            {
                var product = products.FirstOrDefault(x => x.Id == item.ProductId);

                await _api.CreateOrderItemAsync(new CreateOrderItemCommand(item.Quantity, product.Price, orderId, item.ProductId));

                var newQuantity = product.StockQuantity - item.Quantity;
                await _api.UpdateProductBAsync(new UpdateProductCommand(
                          product.Id,
                          product.Name,
                          product.Description, 
                          product.Price,
                          newQuantity,        
                          product.ProductImage,
                          product.CategoryId
                ));

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

            //var token = HttpContext.Session.GetString("Token");
            var token = HttpContext.Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var customerId = Guid.Parse(jwt.Claims.First(c => c.Type == "CustomerId").Value);

            var order = await _api.GetOrderByIdAndCustomerIdAsync(null, customerId);
            if (order == null)
            {
                return View("OrderNotFound");
            }

            return View(order);
        }



        [HttpPost]
        public async Task<IActionResult> OrderTracking(Guid? orderId)
        {
            //var token = HttpContext.Session.GetString("Token");
            var token = HttpContext.Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var customerId = Guid.Parse(jwt.Claims.First(c => c.Type == "CustomerId").Value);

            var order = await _api.GetOrderByIdAndCustomerIdAsync(orderId, customerId);
            if (order == null)
                return View("OrderNotFound");

            return View(order);
        }
    }
}
