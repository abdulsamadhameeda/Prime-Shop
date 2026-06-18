using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Customer.Dtos;


namespace PrimeShop.Application.features.Customer.Commands
{
    internal class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {

        private readonly IRepository<Domain.Entities.Customer> _customerRepo;

        public CreateCustomerHandler(IRepository<Domain.Entities.Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {

            var customer = new Domain.Entities.Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
              

            };
            _customerRepo.Add(customer);

            await _customerRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return customer.Id;

        }

    }
}
