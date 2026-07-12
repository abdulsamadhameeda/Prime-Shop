using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Order.Dtos;


namespace PrimeShop.Application.features.Order.Queries
{
    internal class TotalProfitHandler : IRequestHandler<TotalProfitQueries, decimal>
    {
        private readonly IRepository<Domain.Entities.Order> _OrderRepo;

        public TotalProfitHandler(IRepository<Domain.Entities.Order> OrderRepo)
        {
            _OrderRepo = OrderRepo;
        }
        public async Task<decimal> Handle(TotalProfitQueries request, CancellationToken cancellationToken)
        {

            string filePath = Path.Combine(AppContext.BaseDirectory, "SQL", "TotalProfit.sql");
            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _OrderRepo.QuerySql<int>(sqlQuery, null);

            return result.FirstOrDefault();



        }
    }
}