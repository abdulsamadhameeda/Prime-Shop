using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Order.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Order.Queries
{
    internal class OrdersPerMonthHandler :IRequestHandler<GetOrdersPerMonthQuery, List<OrdersPerMonthDto>>
    {
        private readonly IRepository<Domain.Entities.Order> _OrderRepo;

        public OrdersPerMonthHandler(IRepository<Domain.Entities.Order> OrderRepo)
        {
            _OrderRepo = OrderRepo;
        }
        public async Task<List<OrdersPerMonthDto>> Handle(GetOrdersPerMonthQuery request, CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "SQL", "OrdersPerMonth.sql");
            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _OrderRepo.QuerySql<OrdersPerMonthDto>(sqlQuery, new { year = request.year });

            return result.ToList();
        }

    }
}
