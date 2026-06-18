using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.BasketItem.Dtos
{
  
    public record GetByIdBasketItemQueries(Guid id) : IRequest<BasketItemDto>;

}
