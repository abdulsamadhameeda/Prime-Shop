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
            var customer = await _customerRepo.GetAsync(request.id);
            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
        }



    }
}
