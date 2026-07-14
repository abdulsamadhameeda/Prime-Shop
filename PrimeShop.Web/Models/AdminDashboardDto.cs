using PrimeShop.Application.features.Customer.Dtos;
using PrimeShop.Application.features.Order.Dtos;

namespace PrimeShop.Web.Models
{
    public class AdminDashboardDto
    {
        public int TotalOrders { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalProducts { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal TodaysSales { get; set; }
        public List<OrdersPerMonthDto> OrdersPerMonth { get; set; }
        public List<OrderStatusDto> OrderStatus { get; set; }
        public List<CustomerDto> Cutomers { get; set; }

    }
}
