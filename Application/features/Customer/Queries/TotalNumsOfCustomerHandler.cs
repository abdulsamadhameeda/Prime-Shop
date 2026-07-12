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

        private readonly IRepository<Domain.Entities.Customer> _customerRepo;

        public TotalNumsOfCustomerHandler(IRepository<Domain.Entities.Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }
        public async Task<int> Handle(TotalNumsOfCustomerQueries request, CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "SQL", "TotalNumsOfCustomer.sql");
            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _customerRepo.QuerySql<int>(sqlQuery, null);

            return result.FirstOrDefault();
        }
    }
}
