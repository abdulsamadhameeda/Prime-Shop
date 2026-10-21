using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Customer.Dtos
{
  
    public record UpdateCustomerCommand(
        string Id,
 string FirstName,
 string LastName,
 string Email,
 string PhoneNumber
) : IRequest<string>;
}
