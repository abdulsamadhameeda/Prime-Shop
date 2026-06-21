using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Category.Dtos;
using PrimeShop.Application.features.Product.Dtos;
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
            var product = await _ProductRepo.GetAsync(request.id);
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId
            };
        }

    }
}
