using MediatR;
using PrimeShop.Application.features.Basket.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.BasketItem.Dtos
{
    public record GetAllBasketItemQueries : IRequest<List<BasketItemDto>>;
  
}
