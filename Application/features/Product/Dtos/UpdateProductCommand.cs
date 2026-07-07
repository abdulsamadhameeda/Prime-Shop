using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Product.Dtos
{

    public record UpdateProductCommand(
        Guid Id,
  string Name,
  string Description,
  decimal Price,
  int StockQuantity,
  byte[]? ImageUrl,
  Guid CategoryId
  ) : IRequest<Guid>;
}
