using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Product.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Product.Queries 
{
    public class GetProductsByIdsHandler :IRequestHandler<GetProductsByIdsQuery, List<ProductDto>>
    {
        private readonly IRepository<Domain.Entities.Product> _productRepo;

        public GetProductsByIdsHandler(IRepository<Domain.Entities.Product> productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<List<ProductDto>> Handle(GetProductsByIdsQuery request, CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "SQL", "GetProductsByIds.sql");
            string sqlQuery = await File.ReadAllTextAsync(filePath);
            var result = await _productRepo.QuerySql<ProductDto>(sqlQuery, new { Ids = string.Join(",", request.Ids) });
            return result.ToList();
        }
    }
}
