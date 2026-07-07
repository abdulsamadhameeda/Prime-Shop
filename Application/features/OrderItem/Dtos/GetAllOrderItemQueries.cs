using MediatR;


namespace PrimeShop.Application.features.OrderItem.Dtos
{
  
    public record GetAllOrderItemQueries(Guid? orderId): IRequest<List<OrderItemDto>>;

}   
