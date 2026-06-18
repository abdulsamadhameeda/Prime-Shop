using MediatR;
using Microsoft.EntityFrameworkCore;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Product.Dtos;


namespace PrimeShop.Application.features.Product.Queries
{
    public class GetAllProductHandler : IRequestHandler<GetAllProductQueries, List<ProductDto>>
    {
        private readonly IRepository<Domain.Entities.Product> _categoryRepo;

        public GetAllProductHandler(IRepository<Domain.Entities.Product> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public async Task<List<ProductDto>> Handle(GetAllProductQueries request, CancellationToken cancellationToken)
        {
            var product = await _categoryRepo.GetAll()
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price=x.Price,
                    StockQuantity=x.StockQuantity,
                    ImageUrl =x.ImageUrl,
                    CategoryId=x.CategoryId
                }).ToListAsync();


            return product;


        }
    }
}
