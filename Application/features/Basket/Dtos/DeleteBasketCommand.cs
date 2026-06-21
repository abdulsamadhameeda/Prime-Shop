using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Basket.Dtos
{

    public record DeleteBasketCommand(

     Guid id

) : IRequest<bool>;
}
