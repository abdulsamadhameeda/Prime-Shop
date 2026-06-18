using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Basket.Dtos;


namespace PrimeShop.Application.features.Basket.Commands
{
    internal class DeleteBasketHandler : IRequestHandler<DeleteBasketCommand, Guid>
    {

        private readonly IRepository<Domain.Entities.Basket> _BasketRepo;

        public DeleteBasketHandler(IRepository<Domain.Entities.Basket> BasketRepo)
        {
            _BasketRepo = BasketRepo;
        }
        public async Task<Guid> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await _BasketRepo.GetAsync(request.id);

            if (basket != null)
            {
                _BasketRepo.Delete(basket);
            }

            await _BasketRepo.UnitOfWork.SaveChangesAsync(cancellationToken);
            return basket.Id;
        }

    }
}
