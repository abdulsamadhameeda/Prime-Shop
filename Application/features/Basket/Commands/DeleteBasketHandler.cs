using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Basket.Dtos;


namespace PrimeShop.Application.features.Basket.Commands
{
    internal class DeleteBasketHandler : IRequestHandler<DeleteBasketCommand, bool>
    {

        private readonly IRepository<Domain.Entities.Basket> _BasketRepo;

        public DeleteBasketHandler(IRepository<Domain.Entities.Basket> BasketRepo)
        {
            _BasketRepo = BasketRepo;
        }
        public async Task<bool> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await _BasketRepo.GetAsync(request.Id);

            if (basket == null)
            {
                throw new Exception();
            }
            _BasketRepo.Delete(basket);

            await _BasketRepo.UnitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
