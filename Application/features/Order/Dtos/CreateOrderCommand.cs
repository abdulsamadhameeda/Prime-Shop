using MediatR;
using PrimeShop.Domain.Enums;

namespace PrimeShop.Application.features.Order.Dtos
{
   
    public record CreateOrderCommand(

         DateTime OrderDate,


         decimal TotalAmount,

         OrderStatus Status,


         string ShippingStreet,


         string ShippingCity,


         Guid CustomerId
 ) : IRequest<Guid>;
}
