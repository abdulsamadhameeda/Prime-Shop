using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Order.Dtos
{
    public class OrdersPerMonthDto
    {
        public int Month { get; set; }
        public int TotalOrders { get; set; }
    }
}
