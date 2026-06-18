using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Basket.Dtos;
using PrimeShop.Application.features.BasketItem.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.BasketItem.Commands
{
    internal class CreateBasketItemHandler : IRequestHandler<CreateBasketItemCommand, Guid>
    {

        private readonly IRepository<Domain.Entities.BasketItem> _BasketItemRepo;

        public CreateBasketItemHandler(IRepository<Domain.Entities.BasketItem> BasketItemRepo)
        {
            _BasketItemRepo = BasketItemRepo;
        }
        public async Task<Guid> Handle(CreateBasketItemCommand request, CancellationToken cancellationToken)
        {
            var BasketItem = new Domain.Entities.BasketItem
            {
                BasketId = request.BasketId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,

            };

            _BasketItemRepo.Add(BasketItem);

            await _BasketItemRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return BasketItem.Id;
        }

    }
}
