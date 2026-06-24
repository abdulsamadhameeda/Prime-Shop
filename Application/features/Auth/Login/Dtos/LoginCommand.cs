using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Auth.Login.Dtos
{
    public record LoginCommand(
         string Email,
         string Password
     ) : IRequest<string>;
}
