using MediatR;
using PrimeShop.Domain.Enums;
using System.Text.Json.Serialization;

namespace PrimeShop.Application.features.Order.Dtos
{

    public record CreateOrderCommand(

         DateTime OrderDate,


         decimal TotalAmount,


        [property: JsonConverter(typeof(JsonStringEnumConverter))] 
        OrderStatus Status,

         string ShippingStreet,


         Guid ShippingCityId,


             string UserId
 ) : IRequest<Guid>;
}
