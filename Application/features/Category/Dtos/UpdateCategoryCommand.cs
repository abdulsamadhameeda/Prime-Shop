using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Category.Dtos
{
    public record UpdateCategoryCommand
    (
        Guid categoryId,
        string Name,
        string Description
    ) : IRequest<Guid>;
}
