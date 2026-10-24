using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Contact.Dtos
{
    public record CreateContactMessageCommand(
       string Name,
       string Email,
       string Subject,
       string Message
   ) : IRequest<Guid>;
}
