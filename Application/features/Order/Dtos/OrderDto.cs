using PrimeShop.Domain.Enums;

namespace PrimeShop.Application.features.Order.Dtos
{
    internal class OrderDto
    {
        public Guid Id { get; set; }

        public DateTime OrderDate { get; set; }

       
        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; }

       
        public string ShippingStreet { get; set; }

       
        public string ShippingCity { get; set; }


        public Guid CustomerId { get; set; }

    }
}
