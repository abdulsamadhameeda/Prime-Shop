using MediatR;
using Microsoft.EntityFrameworkCore;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Category.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Category.Queries
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoryQueries , List <CategoryDto>>
    {

        private readonly IRepository<Domain.Entities.Category> _categoryRepo;

        public GetAllCategoriesHandler(IRepository<Domain.Entities.Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public async Task<List<CategoryDto>> Handle(GetAllCategoryQueries request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepo.GetAll()
                .Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                }).ToListAsync();
             

                return category;
        }
    }
}
