using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Basket.Dtos;


namespace PrimeShop.Application.features.Basket.Commands
{
    internal class CreateBasketHandler : IRequestHandler<CreateBasketCommand, Guid>
    {

        private readonly IRepository<Domain.Entities.Basket> _BasketRepo;

        public CreateBasketHandler(IRepository<Domain.Entities.Basket> BasketRepo)
        {
            _BasketRepo = BasketRepo;
        }

        public async Task<Guid> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = new Domain.Entities.Basket
            {
                CreatedAt = DateTime.Now,
                UserId = request.UserId

            };

            _BasketRepo.Add(basket);

            await _BasketRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return basket.Id;
        }


    }   
}
