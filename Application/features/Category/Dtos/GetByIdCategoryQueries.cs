using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Category.Dtos
{
    public record GetByIdCategoryQueries(Guid id) : IRequest<CategoryDto>;
   
}
