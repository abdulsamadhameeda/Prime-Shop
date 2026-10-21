using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Customer.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Customer.Queries
{
    internal class TotalNumsOfCustomerHandler : IRequestHandler<TotalNumsOfCustomerQueries, int>
    {


        private readonly ISqlQueryService _sql;

        public TotalNumsOfCustomerHandler(ISqlQueryService sql)
        {
            _sql = sql;
        }

        public async Task<int> Handle(TotalNumsOfCustomerQueries request, CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "SQL", "TotalNumsOfCustomer.sql");
            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _sql.QuerySql<int>(sqlQuery, null);

            return result.FirstOrDefault();
        }
    }
}
