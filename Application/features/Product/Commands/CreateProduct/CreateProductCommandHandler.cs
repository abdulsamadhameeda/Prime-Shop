using MediatR;
using Microsoft.EntityFrameworkCore;
using PrimeShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Product.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IApplicationDbContext _DbContext;
    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _DbContext = context;
    }
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Domain.Entities.Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            ImageUrl = request.ImageUrl,          
            CategoryId = request.CategoryId       
        };

        _DbContext.Products.Add(product);

        await _DbContext.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}

