using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Order.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Order.Queries
{

    public class GetOrderStatusHandler : IRequestHandler<GetOrderStatusQuery, List<OrderStatusDto>>
    {
        private readonly IRepository<Domain.Entities.Order> _OrderRepo;

        public GetOrderStatusHandler(IRepository<Domain.Entities.Order> orderRepo)
        {
            _OrderRepo = orderRepo;
        }

        public async Task<List<OrderStatusDto>> Handle(GetOrderStatusQuery request, CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "SQL", "OrderStatus.sql");
            string sqlQuery = await File.ReadAllTextAsync(filePath);
            var result = await _OrderRepo.QuerySql<OrderStatusDto>(sqlQuery);
            return result.ToList();
        }
    }
}
