using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.BasketItem.Dtos
{

    public record UpdateBasketItemCommand(
              Guid Id,

   int Quantity,

    Guid BasketId,



    Guid ProductId

) : IRequest<Guid>;
}
