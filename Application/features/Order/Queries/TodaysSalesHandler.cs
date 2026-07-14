using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Order.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Order.Queries
{
    internal class TodaysSalesHandler : IRequestHandler<TodaysSalesQueries, decimal>
    {
        private readonly IRepository<Domain.Entities.Order> _OrderRepo;

        public TodaysSalesHandler(IRepository<Domain.Entities.Order> OrderRepo)
        {
            _OrderRepo = OrderRepo;
        }
        public async Task<decimal> Handle(TodaysSalesQueries request, CancellationToken cancellationToken)
        {

            string filePath = Path.Combine(AppContext.BaseDirectory, "SQL", "TodaysSales.sql");
            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _OrderRepo.QuerySql<int>(sqlQuery);

            return result.FirstOrDefault();




        }
    }
}
