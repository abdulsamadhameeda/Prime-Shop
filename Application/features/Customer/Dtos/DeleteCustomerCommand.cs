using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Customer.Dtos
{
  
    public record DeleteCustomerCommand
(
    Guid id

) : IRequest<Guid>;
}
