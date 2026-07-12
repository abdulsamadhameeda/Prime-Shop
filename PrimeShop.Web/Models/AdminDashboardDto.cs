namespace PrimeShop.Web.Models
{
    public class AdminDashboardDto
    {
        public int TotalOrders { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalProducts { get; set; }
        public decimal TotalProfit { get; set; }
    }
}
