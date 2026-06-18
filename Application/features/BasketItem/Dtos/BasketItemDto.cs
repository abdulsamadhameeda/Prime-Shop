

namespace PrimeShop.Application.features.BasketItem.Dtos
{
    internal class BasketItemDto
    {
        public Guid Id { get; set; }

        
        public int Quantity { get; set; }

       

        public Guid BasketId { get; set; }



        public Guid ProductId { get; set; }

    }
}
