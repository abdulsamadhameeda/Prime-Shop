using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Basket.Dtos;
using PrimeShop.Application.features.BasketItem.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.BasketItem.Commands
{
    internal class DeleteBasketItemHandler : IRequestHandler<DeleteBasketItemCommand, bool>
    {

        private readonly IRepository<Domain.Entities.BasketItem> _BasketItemRepo;

        public DeleteBasketItemHandler(IRepository<Domain.Entities.BasketItem> BasketItemRepo)
        {
            _BasketItemRepo = BasketItemRepo;
        }
        public async Task<bool> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basketItem = await _BasketItemRepo.GetAsync(request.id);

            if (basketItem == null)
            {
                throw new Exception();
            }
            _BasketItemRepo.Delete(basketItem);

            await _BasketItemRepo.UnitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
