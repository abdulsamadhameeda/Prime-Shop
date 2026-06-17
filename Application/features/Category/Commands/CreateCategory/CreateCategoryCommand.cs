using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Category.Commands.CreateCategory;

public record CreateCategoryCommand(
    string Name,
    string Description
    
) : IRequest<Guid>;