using MediatR;
using PrimeShop.Application.features.Category.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Product.Dtos
{
    public record GetAllProductQueries(string? name) : IRequest<List<ProductDto>>
    {
      
       
    }
}
