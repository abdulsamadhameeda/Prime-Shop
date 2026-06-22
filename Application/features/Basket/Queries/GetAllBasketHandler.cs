using MediatR;
using Microsoft.EntityFrameworkCore;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Basket.Dtos;
using PrimeShop.Application.features.Category.Dtos;
using PrimeShop.Application.features.Product.Dtos;
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
            string filePath = Path.Combine(AppContext.BaseDirectory, "features", "Basket", "Queries", "SQL", "GetAllBasket.sql");

            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _BasketRepo.QuerySql<BasketDto>(sqlQuery);

            return result.ToList();
        }
    }
}
