using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Product.Dtos
{
   
    public record DeleteProductCommand
  (
      Guid id

  ) : IRequest<Guid>;
}
