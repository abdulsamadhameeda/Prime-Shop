using MediatR;
using PrimeShop.Application.features.Category.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Basket.Dtos
{
    
    public record GetAllBasketQueries : IRequest<List<BasketDto>>;

}
