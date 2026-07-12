using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Product.Dtos
{
    public record TotalNumsOfProductQuery() : IRequest<int>;    
    
}
