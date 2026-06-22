using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Order.Dtos;
using PrimeShop.Application.features.OrderItem.Dtos;
using PrimeShop.Application.features.Product.Dtos;
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
            string filePath = Path.Combine(AppContext.BaseDirectory, "features", "OrderItem", "Queries", "SQL", "GetByIdOrderItem.sql");

            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _OrderItemRepo.QuerySql<OrderItemDto>(sqlQuery, new { Id = request.id });

            return result.FirstOrDefault();
        }

    }
}
