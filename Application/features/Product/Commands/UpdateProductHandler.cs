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

        private readonly IRepository<Domain.Entities.Product> _categoryRepo;

    public UpdateProductHandler(IRepository<Domain.Entities.Product> categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {


            var product = await _categoryRepo.GetAsync(request.productId);

            if (product != null)
            {
                product.Name = request.Name;
                product.Description = request.Description;
                product.Price = request.Price;
                product.StockQuantity = request.StockQuantity;
                product.ImageUrl = request.ImageUrl;
                product.CategoryId = request.CategoryId;
            
            }
       
            _categoryRepo.Update(product);

            await _categoryRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return product.Id;

    }
}
    
}

