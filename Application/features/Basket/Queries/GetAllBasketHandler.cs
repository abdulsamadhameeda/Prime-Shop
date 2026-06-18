using MediatR;
using Microsoft.EntityFrameworkCore;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Basket.Dtos;
using PrimeShop.Application.features.Category.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Basket.Queries
{
    internal class GetAllBasketHandler : IRequestHandler<GetAllBasketQueries, List<BasketDto>>
    {

        private readonly IRepository<Domain.Entities.Basket> _BasketRepo;

        public GetAllBasketHandler(IRepository<Domain.Entities.Basket> BasketRepo)
        {
            _BasketRepo = BasketRepo;
        }
        public async Task<List<BasketDto>> Handle(GetAllBasketQueries request, CancellationToken cancellationToken)
        {
            var Basket = await _BasketRepo.GetAll()
                .Select(x => new BasketDto
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt,
                    CustomerId = x.CustomerId,
                }).ToListAsync();


            return Basket;
        }
    }
}
