using MediatR;
using Microsoft.EntityFrameworkCore;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Customer.Dtos;
using PrimeShop.Application.features.Product.Dtos;


namespace PrimeShop.Application.features.Customer.Queries
{
    internal class GetAllCustomerHandler : IRequestHandler<GetAllCustomerQueries, List<CustomerDto>>
    {

        private readonly IRepository<Domain.Entities.Customer> _customerRepo;

        public GetAllCustomerHandler(IRepository<Domain.Entities.Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<List<CustomerDto>> Handle(GetAllCustomerQueries request, CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "features", "Customer", "Queries", "SQL", "GetAllCustomer.sql");

            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _customerRepo.QuerySql<CustomerDto>(sqlQuery, new { Name = request.name });

            return result.ToList();


        }
    }
}