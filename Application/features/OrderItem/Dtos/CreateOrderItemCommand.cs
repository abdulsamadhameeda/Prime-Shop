using MediatR;


namespace PrimeShop.Application.features.OrderItem.Dtos
{
    public record CreateOrderItemCommand(

         int Quantity ,

         decimal UnitPrice ,

         Guid OrderId ,

         Guid ProductId 

    ) : IRequest<Guid>;
}
