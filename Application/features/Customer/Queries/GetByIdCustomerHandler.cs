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

        private readonly IRepository<Domain.Entities.Customer> _customerRepo;

        public GetByIdCustomerHandler(IRepository<Domain.Entities.Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }
        public async Task<CustomerDto> Handle(GetByIdCustomerQueries request, CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "features", "Customer", "Queries", "SQL", "GetByIdCustomer.sql");

            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _customerRepo.QuerySql<CustomerDto>(sqlQuery, new { Id = request.id });

            return result.FirstOrDefault();
        }



    }
}
