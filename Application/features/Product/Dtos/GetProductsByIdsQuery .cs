using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Product.Dtos
{
    public record GetProductsByIdsQuery(List<Guid> Ids) : IRequest<List<ProductDto>>;
}
