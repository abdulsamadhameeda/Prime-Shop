using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Customer.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Customer.Queries
{
    internal class Top6NewCustomersHandler : IRequestHandler<Top6NewCustomersQueries, List<CustomerDto>>
    {


        private readonly ISqlQueryService _sql;

        public Top6NewCustomersHandler(ISqlQueryService sql)
        {
            _sql = sql;
        }

        public async Task<List<CustomerDto>> Handle(Top6NewCustomersQueries request, CancellationToken cancellationToken)
        {

            string filePath = Path.Combine(AppContext.BaseDirectory, "SQL", "Top6NewCustomers.sql");

            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _sql.QuerySql<CustomerDto>(sqlQuery);

            return result.ToList();


        }
    }
}
