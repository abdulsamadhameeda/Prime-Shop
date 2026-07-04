using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Category.Dtos;
using PrimeShop.Application.features.Product.Dtos;
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
            string filePath = Path.Combine(AppContext.BaseDirectory, "SQL", "GetByIdCategory.sql");

            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _categoryRepo.QuerySql<CategoryDto>(sqlQuery, new { Id = request.id });

            return result.FirstOrDefault();
        }
    }
}

