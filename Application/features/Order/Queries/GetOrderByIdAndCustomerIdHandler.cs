using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Order.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Order.Queries
{
    public class GetOrderByIdAndCustomerIdHandler : IRequestHandler<GetOrderByIdAndCustomerIdQuery,List<OrderDto>>
    {
        private readonly IRepository<Domain.Entities.Order> _OrderRepo;

        public GetOrderByIdAndCustomerIdHandler(IRepository<Domain.Entities.Order> OrderRepo)
        {
            _OrderRepo = OrderRepo;
        }
        public async Task<List<OrderDto>> Handle(GetOrderByIdAndCustomerIdQuery request, CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "SQL", "GetOrderByIdAndCustomerId.sql");
            string sqlQuery = await File.ReadAllTextAsync(filePath);
            var result = await _OrderRepo.QuerySql<OrderDto>(sqlQuery, new { OrderId = request.OrderId, CustomerId = request.CustomerId });

            return result.ToList();

        }
    }
}
