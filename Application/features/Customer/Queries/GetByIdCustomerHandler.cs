using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Customer.Dtos;
using PrimeShop.Application.features.Product.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Customer.Queries
{
    internal class GetByIdCustomerHandler : IRequestHandler<GetByIdCustomerQueries, CustomerDto>
    {

        private readonly ISqlQueryService _sql;

        public GetByIdCustomerHandler(ISqlQueryService sql)
        {
            _sql = sql;
        }
        public async Task<CustomerDto> Handle(GetByIdCustomerQueries request, CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "SQL", "GetByIdCustomer.sql");

            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _sql.QuerySql<CustomerDto>(sqlQuery, new { Id = request.id });

            return result.FirstOrDefault();
        }



    }
}
