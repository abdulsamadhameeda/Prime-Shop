using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Order.Dtos;

namespace PrimeShop.Application.features.Order.Queries
{
    internal class GetByIdOrderHandler : IRequestHandler<GetByIdOrderQueries, OrderDto>
    {

        private readonly IRepository<Domain.Entities.Order> _OrderRepo;

        public GetByIdOrderHandler(IRepository<Domain.Entities.Order> OrderRepo)
        {
            _OrderRepo = OrderRepo;
        }
        public async Task<OrderDto> Handle(GetByIdOrderQueries request, CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "SQL", "GetByIdOrder.sql");

            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _OrderRepo.QuerySql<OrderDto>(sqlQuery, new { Id = request.id });

            return result.FirstOrDefault();
        }

    }
}
