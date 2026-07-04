using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.BasketItem.Dtos;
using PrimeShop.Application.features.Product.Dtos;


namespace PrimeShop.Application.features.BasketItem.Queries
{
    public class GetByBasketIdBasketItemHandler : IRequestHandler<GetByIdBasketItemQueries, List<BasketItemDto>>
    {

        private readonly IRepository<Domain.Entities.BasketItem> _BasketItemRepo;

        public GetByBasketIdBasketItemHandler(IRepository<Domain.Entities.BasketItem> BasketItemRepo)
        {
            _BasketItemRepo = BasketItemRepo;
        }
        public async Task<List<BasketItemDto>> Handle(GetByIdBasketItemQueries request, CancellationToken cancellationToken)
        {

            string filePath = Path.Combine(AppContext.BaseDirectory, "SQL", "GetByBasketIdBasketItem.sql");

            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _BasketItemRepo.QuerySql<BasketItemDto>(sqlQuery, new { BasketId = request.BasketId });

            return result.ToList();
        }

    }
}
