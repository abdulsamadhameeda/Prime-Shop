using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Category.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Category.Queries
{
    public class GetByIdCategoryHandler : IRequestHandler<GetByIdCategoryQueries, CategoryDto>
    {

        private readonly IRepository<Domain.Entities.Category> _categoryRepo;

        public GetByIdCategoryHandler(IRepository<Domain.Entities.Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public async Task<CategoryDto> Handle(GetByIdCategoryQueries request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepo.GetAsync(request.id);
            return new CategoryDto
            {
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}

