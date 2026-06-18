using MediatR;


namespace PrimeShop.Application.features.OrderItem.Dtos
{
  
    public class GetAllOrderItemQueries : IRequest<List<OrderItemDto>>;

}
