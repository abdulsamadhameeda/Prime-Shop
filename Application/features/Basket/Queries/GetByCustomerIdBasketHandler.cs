using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Basket.Dtos;

namespace PrimeShop.Application.features.Basket.Queries
{
    internal class GetByCustomerIdBasketHandler : IRequestHandler<GetByCustomerIdBasketQueries, BasketDto>
    {

        private readonly IRepository<Domain.Entities.Basket> _BasketRepo;

        public GetByCustomerIdBasketHandler(IRepository<Domain.Entities.Basket> BasketRepo)
        {
            _BasketRepo = BasketRepo;
        }
        public async Task<BasketDto> Handle(GetByCustomerIdBasketQueries request, CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "SQL", "GetByUserIdBasket.sql");

            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _BasketRepo.QuerySql<BasketDto>(sqlQuery , new { UserId = request.UserId }   );

            return result.FirstOrDefault();
        }

    }
}
