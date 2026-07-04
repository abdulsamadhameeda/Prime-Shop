using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Product.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Product.Queries
{
    public class GetRandomProductsHandler  : IRequestHandler<GetRandomProductsQuery, List<ProductDto>>
    {
        private readonly IRepository<Domain.Entities.Product> _ProductRepo;

        public GetRandomProductsHandler(IRepository<Domain.Entities.Product> ProductRepo)
        {
            _ProductRepo = ProductRepo;
        }

        public async Task<List<ProductDto>> Handle(GetRandomProductsQuery request, CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "SQL", "GetRandomProducts.sql");
            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _ProductRepo.QuerySql<ProductDto>(sqlQuery, new { Count = request.Count });

            return result.ToList();
        }


    }
}
