using MediatR;
using PrimeShop.Application.features.Product.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Order.Dtos
{


    public record GetByIdOrderQueries(Guid id) : IRequest<OrderDto>;

}
