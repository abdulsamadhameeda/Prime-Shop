using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Basket.Dtos
{
  
    public record GetByCustomerIdBasketQueries(string UserId) : IRequest<BasketDto>;

}
