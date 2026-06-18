using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.BasketItem.Dtos;


namespace PrimeShop.Application.features.BasketItem.Commands
{
    internal class UpdateBasketItemHandler : IRequestHandler<UpdateBasketItemCommand, Guid>
    {

        private readonly IRepository<Domain.Entities.BasketItem> _BasketItemRepo;

        public UpdateBasketItemHandler(IRepository<Domain.Entities.BasketItem> BasketItemRepo)
        {
            _BasketItemRepo = BasketItemRepo;
        }
        public async Task<Guid> Handle(UpdateBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basketItem = await _BasketItemRepo.GetAsync(request.Id);

            if (basketItem != null)
            {
                basketItem.ProductId = request.ProductId;
                basketItem.BasketId = request.BasketId;
                basketItem.Quantity = request.Quantity;
            }

            _BasketItemRepo.Update(basketItem);

            await _BasketItemRepo.UnitOfWork.SaveChangesAsync(cancellationToken);
            return basketItem.Id;
        }

    }
}
