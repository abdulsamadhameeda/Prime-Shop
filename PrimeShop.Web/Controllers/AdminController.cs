using Microsoft.AspNetCore.Mvc;
using PrimeShop.Web.Interface;
using PrimeShop.Web.Models;

namespace PrimeShop.Web.Controllers
{
    public class AdminController : Controller
    {

        private readonly IPrimeShopApi _api;

        public AdminController(IPrimeShopApi api)
        {
            _api = api;
        }

        public async Task<IActionResult> Dashboard()
        {
            var currentYear = DateTime.Now.Year;
            var ordersPerMonth = await _api.GetOrdersPerMonthAsync(currentYear);

            var orderStatus = await _api.GetOrderStatusAsync();

            var totalOrders = await _api.GetTotalOrdersAsync();
            var totalCustomers = await _api.GetTotalCustomersAsync();
            var totalProducts = await _api.GetTotalProductsAsync();
            var totalProfit = await _api.GetTotalProfitAsync();
            var todaysSales = await _api.GetTodaysSalesAsync();
            var top6Customer = await _api.GetTop6NewCustomersAsync();

            var dashboard = new AdminDashboardDto
            {
                TotalOrders = totalOrders,
                TotalCustomers = totalCustomers,
                TotalProducts = totalProducts,  
                TotalProfit = totalProfit,
                OrdersPerMonth = ordersPerMonth,
                OrderStatus = orderStatus ,
                TodaysSales = todaysSales,
                Cutomers = top6Customer

            };

            return View(dashboard);
        }


        [HttpGet]
        public async Task<IActionResult> GetOrdersPerMonth(int year)
        {
            var data = await _api.GetOrdersPerMonthAsync(year);
            return Json(data);
        }


        [HttpGet]
        public async Task<IActionResult> OrderManagment()
        {
            var model = new TableViewModel
            {
                Columns = new List<string> { "Id", "OrderDate", "TotalAmount", "Status" , "ShippingStreet" , "ShippingCity" }
            };
            return View(model);
        }





    }
}
