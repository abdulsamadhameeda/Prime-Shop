using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Basket.Dtos;
using PrimeShop.Application.features.Category.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Basket.Queries
{
    internal class GetByIdBasketHandler : IRequestHandler<GetByIdBasketQueries, BasketDto>
    {

        private readonly IRepository<Domain.Entities.Basket> _BasketRepo;

        public GetByIdBasketHandler(IRepository<Domain.Entities.Basket> BasketRepo)
        {
            _BasketRepo = BasketRepo;
        }
        public async Task<BasketDto> Handle(GetByIdBasketQueries request, CancellationToken cancellationToken)
        {
            var basket = await _BasketRepo.GetAsync(request.id);
            return new BasketDto
            {
                Id= basket.Id,
                CreatedAt = basket.CreatedAt,
                CustomerId = basket.CustomerId
            };
        }

    }
}
