using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.OrderItem.Dtos
{
   
    public record DeleteOrderItemCommand(
      Guid Id
      

 ) : IRequest<bool>;
}
