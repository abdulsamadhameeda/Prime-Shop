using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Customer.Dtos
{
  
    public record UpdateCustomerCommand(
 Guid Id,
 string FirstName,
 string LastName,
 string Email,
 string PhoneNumber
) : IRequest<Guid>;
}
