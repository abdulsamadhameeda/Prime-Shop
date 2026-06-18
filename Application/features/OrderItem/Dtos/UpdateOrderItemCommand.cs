using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.OrderItem.Dtos
{
 
    public record UpdateOrderItemCommand(
        Guid id,
        int Quantity,

        decimal UnitPrice,

        Guid OrderId,

        Guid ProductId

   ) : IRequest<Guid>;
}
