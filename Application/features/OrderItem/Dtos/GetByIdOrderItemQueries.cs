using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.OrderItem.Dtos
{
    
    public record GetByIdOrderItemQueries(Guid id) : IRequest<OrderItemDto>;

}
