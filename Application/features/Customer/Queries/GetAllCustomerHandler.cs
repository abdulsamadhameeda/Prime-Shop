using MediatR;
using Microsoft.EntityFrameworkCore;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Customer.Dtos;


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
            var customer = await _customerRepo.GetAll()
                .Select(x => new CustomerDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email= x.Email,
                    PhoneNumber = x.PhoneNumber
                    
                }).ToListAsync();


            return customer;


        }
    }
}