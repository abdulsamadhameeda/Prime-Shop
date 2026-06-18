using MediatR;
using Microsoft.EntityFrameworkCore;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Basket.Dtos;
using PrimeShop.Application.features.BasketItem.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.BasketItem.Queries
{
    internal class GetAllBasketItemHandler : IRequestHandler<GetAllBasketItemQueries, List<BasketItemDto>>
    {

        private readonly IRepository<Domain.Entities.BasketItem> _BasketItemRepo;

        public GetAllBasketItemHandler(IRepository<Domain.Entities.BasketItem> BasketItemRepo)
        {
            _BasketItemRepo = BasketItemRepo;
        }

        public async Task<List<BasketItemDto>> Handle(GetAllBasketItemQueries request, CancellationToken cancellationToken)
        {
            var BasketItem = await _BasketItemRepo.GetAll()
                .Select(x => new BasketItemDto
                {
                    Id = x.Id,
                    BasketId = x.BasketId,
                    ProductId = x.ProductId,
                    Quantity=x.Quantity

                }).ToListAsync();


            return BasketItem;
        }

    }
}
