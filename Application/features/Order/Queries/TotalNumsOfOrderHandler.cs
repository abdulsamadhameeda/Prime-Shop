using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Order.Dtos;


namespace PrimeShop.Application.features.Order.Queries
{
    internal class TotalNumsOfOrderHandler : IRequestHandler<TotalNumsOfOrderQueries, int>
    {
        private readonly IRepository<Domain.Entities.Order> _OrderRepo;

        public TotalNumsOfOrderHandler(IRepository<Domain.Entities.Order> OrderRepo)
        {
            _OrderRepo = OrderRepo;
        }
        public async Task<int> Handle(TotalNumsOfOrderQueries request, CancellationToken cancellationToken)
        {

            string filePath = Path.Combine(AppContext.BaseDirectory, "SQL", "TotalNumsOfOrder.sql");
            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _OrderRepo.QuerySql<int>(sqlQuery, null);

            return result.FirstOrDefault();


        }
    }
}
