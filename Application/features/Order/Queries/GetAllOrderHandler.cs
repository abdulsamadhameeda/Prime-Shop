using MediatR;
using Microsoft.EntityFrameworkCore;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Customer.Dtos;
using PrimeShop.Application.features.Order.Dtos;
using System.Net.NetworkInformation;


namespace PrimeShop.Application.features.Order.Queries
{
    internal class GetAllOrderHandler : IRequestHandler<GetAllOrderQueries, List<OrderDto>>
    {

        private readonly IRepository<Domain.Entities.Order> _OrderRepo;

        public GetAllOrderHandler(IRepository<Domain.Entities.Order> OrderRepo)
        {
            _OrderRepo = OrderRepo;
        }
        public async Task<List<OrderDto>> Handle(GetAllOrderQueries request, CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "features", "Order", "Queries", "SQL", "GetAllOrder.sql");

            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _OrderRepo.QuerySql<OrderDto>(sqlQuery, new { Status = request.status });

            return result.ToList();
        }

    }
}
