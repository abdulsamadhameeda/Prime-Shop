using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Category.Dtos;
using PrimeShop.Application.features.Product.Dtos;
using PrimeShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Product.Queries
{
    internal class GetByIdProductHandler : IRequestHandler<GetByIdProductQueries, ProductDto>
    {
        private readonly IRepository<Domain.Entities.Product> _ProductRepo;

        public GetByIdProductHandler(IRepository<Domain.Entities.Product> ProductRepo)
        {
            _ProductRepo = ProductRepo;
        }
        public async Task<ProductDto> Handle(GetByIdProductQueries request, CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "features", "Product", "Queries", "SQL", "GetByIdProduct.sql");

            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _ProductRepo.QuerySql<ProductDto>(sqlQuery, new { Id = request.id });
           
            return result.FirstOrDefault();
        }

    }
}
