using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Product.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace PrimeShop.Application.features.Product.Commands
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Guid>
    {

        private readonly IRepository<Domain.Entities.Product> _ProductRepo;

    public UpdateProductHandler(IRepository<Domain.Entities.Product> ProductRepo)
    {
            _ProductRepo = ProductRepo;
    }

    public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {


            var product = await _ProductRepo.GetAsync(request.Id);

            if (product != null)
            {
                product.Name = request.Name;
                product.Description = request.Description;
                product.Price = request.Price;
                product.StockQuantity = request.StockQuantity;
                product.ProductImage = request.ImageUrl;
                product.CategoryId = request.CategoryId;
            
            }

            _ProductRepo.Update(product);

            await _ProductRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return product.Id;

    }
}
    
}

