using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Category.Dtos;
using PrimeShop.Application.features.Product.Dtos;
using PrimeShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Product.Commands
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
    {

        private readonly IRepository<Domain.Entities.Product> _ProductRepo;

        public CreateProductHandler(IRepository<Domain.Entities.Product> ProductRepo)
        {
            _ProductRepo = ProductRepo;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            var product = new Domain.Entities.Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                ProductImage = request.ImageUrl,
                CategoryId = request.CategoryId,

            };
            _ProductRepo.Add(product);

            await _ProductRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return product.Id;

        }
    }
}
