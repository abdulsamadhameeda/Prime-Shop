using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Basket.Dtos
{
   
    public record UpdateBasketCommand(
      Guid Id,

     DateTime CreatedAt,

     Guid CustomerId

) : IRequest<Guid>;
}
