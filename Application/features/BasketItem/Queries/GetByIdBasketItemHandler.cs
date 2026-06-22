using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.BasketItem.Dtos;
using PrimeShop.Application.features.Product.Dtos;


namespace PrimeShop.Application.features.BasketItem.Queries
{
    internal class GetByIdBasketItemHandler : IRequestHandler<GetByIdBasketItemQueries, BasketItemDto>
    {

        private readonly IRepository<Domain.Entities.BasketItem> _BasketItemRepo;

        public GetByIdBasketItemHandler(IRepository<Domain.Entities.BasketItem> BasketItemRepo)
        {
            _BasketItemRepo = BasketItemRepo;
        }
        public async Task<BasketItemDto> Handle(GetByIdBasketItemQueries request, CancellationToken cancellationToken)
        {

            string filePath = Path.Combine(AppContext.BaseDirectory, "features", "BasketItem", "Queries", "SQL", "GetByIdBasketItem.sql");

            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _BasketItemRepo.QuerySql<BasketItemDto>(sqlQuery, new { Id = request.id });

            return result.FirstOrDefault();
        }

    }
}
