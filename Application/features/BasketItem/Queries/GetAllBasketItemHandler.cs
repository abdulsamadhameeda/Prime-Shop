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

            string filePath = Path.Combine(AppContext.BaseDirectory, "features", "BasketItem", "Queries", "SQL", "GetAllBasketItem.sql");

            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _BasketItemRepo.QuerySql<BasketItemDto>(sqlQuery);

            return result.ToList();
        }

    }
}
