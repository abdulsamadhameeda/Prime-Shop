

namespace PrimeShop.Application.features.OrderItem.Dtos
{
    internal class OrderItemDto
    {
        public Guid Id { get; set; }
        
        public int Quantity { get; set; }
 
        public decimal UnitPrice { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

      
    }
}
