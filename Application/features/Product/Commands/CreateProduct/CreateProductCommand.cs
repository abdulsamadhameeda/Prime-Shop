using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Product.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    int StockQuantity,
    string ImageUrl,  
    Guid CategoryId   
) : IRequest<Guid>;