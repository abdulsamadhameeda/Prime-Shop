using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Order.Dtos
{

    public record DeleteOrderCommand
(
    Guid Id

) : IRequest<bool>;
}
