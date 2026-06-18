using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.BasketItem.Dtos;


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
            var basketItem = await _BasketItemRepo.GetAsync(request.id);
            return new BasketItemDto
            {
                Id = basketItem.Id,
                BasketId = basketItem.BasketId,
                ProductId = basketItem.ProductId,
                Quantity= basketItem.Quantity


            };
        }

    }
}
