using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Basket.Dtos;
using PrimeShop.Application.features.Category.Dtos;
using PrimeShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Basket.Commands
{
    internal class UpdateBasketHandler : IRequestHandler<UpdateBasketCommand, Guid>
    {

        private readonly IRepository<Domain.Entities.Basket> _BasketRepo;

        public UpdateBasketHandler(IRepository<Domain.Entities.Basket> BasketRepo)
        {
            _BasketRepo = BasketRepo;
        }

        public async Task<Guid> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await _BasketRepo.GetAsync(request.Id);

            if (basket == null)
            {
                throw new KeyNotFoundException($"Basket with id {request.Id} was not found.");
            }

            basket.CreatedAt = request.CreatedAt;
            basket.UserId = request.UserId;

            _BasketRepo.Update(basket);

            await _BasketRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return basket.Id;
        }
    }
}
