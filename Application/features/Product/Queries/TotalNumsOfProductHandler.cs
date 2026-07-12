using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Product.Dtos;

namespace PrimeShop.Application.features.Product.Queries
{
    internal class TotalNumsOfProductHandler : IRequestHandler<TotalNumsOfProductQuery, int>
    {
        private readonly IRepository<Domain.Entities.Product> _ProductRepo;

        public TotalNumsOfProductHandler(IRepository<Domain.Entities.Product> ProductRepo)
        {
            _ProductRepo = ProductRepo;
        }
        public async Task<int> Handle(TotalNumsOfProductQuery request, CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "SQL", "TotalNumsOfProduct.sql");
            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _ProductRepo.QuerySql<int>(sqlQuery, null);

            return result.FirstOrDefault();
        }

    }
}
