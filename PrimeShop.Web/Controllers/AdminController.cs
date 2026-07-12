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

        public async Task<IActionResult> IndexAsync()
        {

            var totalOrders = await _api.GetTotalOrdersAsync();
            var totalCustomers = await _api.GetTotalCustomersAsync();
            var totalProducts = await _api.GetTotalProductsAsync();
            var totalProfit = await _api.GetTotalProfitAsync();

            var dashboard = new AdminDashboardDto
            {
                TotalOrders = totalOrders,
                TotalCustomers = totalCustomers,
                TotalProducts = totalProducts,
                TotalProfit = totalProfit
            };

            return View(dashboard);
        }
    }
}
