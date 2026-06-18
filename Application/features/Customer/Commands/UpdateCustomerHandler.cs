using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Customer.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Customer.Commands
{
    internal class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, Guid>
    {

        private readonly IRepository<Domain.Entities.Customer> _customerRepo;

        public UpdateCustomerHandler(IRepository<Domain.Entities.Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<Guid> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {

            var customer = await _customerRepo.GetAsync(request.Id);

            if (customer != null)
            {
                customer.FirstName = request.FirstName;
                customer.LastName = request.LastName;
                customer.Email = request.Email;
                customer.PhoneNumber = request.PhoneNumber;
             

            }

            _customerRepo.Update(customer);

            await _customerRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return customer.Id;


        }
    }
}
