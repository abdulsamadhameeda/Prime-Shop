using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.Common.Models;
using PrimeShop.Application.features.Product.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Product.Queries
{
    public class GetShopProductsHandler
      : IRequestHandler<GetShopProductsQuery, DataTableResponse<ProductDto>>
    {
        private readonly IRepository<Domain.Entities.Product> _productRepo;

        public GetShopProductsHandler(
            IRepository<Domain.Entities.Product> productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<DataTableResponse<ProductDto>> Handle(
            GetShopProductsQuery request,
            CancellationToken cancellationToken)
        {
            var sqlFolder = Path.Combine(AppContext.BaseDirectory, "SQL");

            var productsSql = await File.ReadAllTextAsync(
                Path.Combine(sqlFolder, "GetShopProducts.sql"));

            var filteredCountSql = await File.ReadAllTextAsync(
                Path.Combine(sqlFolder, "GetShopProductsFilteredCount.sql"));

                var totalCountSql = await File.ReadAllTextAsync(
                    Path.Combine(sqlFolder, "TotalNumsOfProduct.sql"));

            var parameters = new
            {
                request.CategoryId,
                request.MinPrice,
                request.MaxPrice,
                request.SortBy,
                Start = Math.Max(request.Start, 0),
                Length = request.Length <= 0 ? 12 : request.Length
            };

            var data = await _productRepo.QuerySql<ProductDto>(
                productsSql, parameters);

            var recordsFiltered = (await _productRepo.QuerySql<int>(
                filteredCountSql, parameters)).FirstOrDefault();

            var recordsTotal = (await _productRepo.QuerySql<int>(
                totalCountSql, new { })).FirstOrDefault();

            return new DataTableResponse<ProductDto>
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsFiltered,
                Data = data.ToList()
            };
        }
    }
}
