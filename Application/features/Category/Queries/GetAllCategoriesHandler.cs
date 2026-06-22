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
            string filePath = Path.Combine(AppContext.BaseDirectory, "features", "Category", "Queries", "SQL", "GetAllCategory.sql");

            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _categoryRepo.QuerySql<CategoryDto>(sqlQuery);

            return result.ToList();
        }
    }
}
