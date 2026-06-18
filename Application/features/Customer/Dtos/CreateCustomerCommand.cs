using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Customer.Dtos
{

    public record CreateCustomerCommand(

    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
   ) : IRequest<Guid>;
}
