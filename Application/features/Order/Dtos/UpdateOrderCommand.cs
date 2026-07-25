using MediatR;
using PrimeShop.Domain.Enums;


namespace PrimeShop.Application.features.Order.Dtos
{
    
    public record UpdateOrderCommand(
       Guid Id,

         DateTime OrderDate,


         decimal TotalAmount,

         OrderStatus Status,


         string ShippingStreet,


          Guid ShippingCityId,


         Guid CustomerId
 ) : IRequest<Guid>;
}
