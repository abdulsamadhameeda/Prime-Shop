using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Order.Dtos;
using PrimeShop.Application.features.OrderItem.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.OrderItem.Queries
{
    internal class GetByIdOrderItemHandler : IRequestHandler<GetByIdOrderItemQueries, OrderItemDto>
    {

        private readonly IRepository<Domain.Entities.OrderItem> _OrderItemRepo;

        public GetByIdOrderItemHandler(IRepository<Domain.Entities.OrderItem> OrderItemRepo)
        {
            _OrderItemRepo = OrderItemRepo;
        }
        public async Task<OrderItemDto> Handle(GetByIdOrderItemQueries request, CancellationToken cancellationToken)
        {
            var orderItem = await _OrderItemRepo.GetAsync(request.id);
            return new OrderItemDto
            {
                Id = orderItem.Id,
                UnitPrice = orderItem.UnitPrice,
                Quantity = orderItem.Quantity,
                ProductId = orderItem.ProductId,
                OrderId = orderItem.OrderId
            };
        }

    }
}
