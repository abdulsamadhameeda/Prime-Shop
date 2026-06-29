using MediatR;
using Microsoft.EntityFrameworkCore;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Product.Dtos;


namespace PrimeShop.Application.features.Product.Queries
{
    public class GetAllProductHandler : IRequestHandler<GetAllProductQueries, List<ProductDto>>
    {
        private readonly IRepository<Domain.Entities.Product> _ProductRepo;

        public GetAllProductHandler(IRepository<Domain.Entities.Product> ProductRepo)
        {
            _ProductRepo = ProductRepo;
        }
        public async Task<List<ProductDto>> Handle(GetAllProductQueries request, CancellationToken cancellationToken)
        {
          
            string filePath = Path.Combine(AppContext.BaseDirectory, "features", "Product", "Queries", "SQL", "GetAllProduct.sql");

            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _ProductRepo.QuerySql<ProductDto>(sqlQuery , new
            {
                CategoryId = request.CategoryId,
                MinPrice = request.MinPrice,
                MaxPrice = request.MaxPrice
            });

            return result.ToList();
        }
    }
}
