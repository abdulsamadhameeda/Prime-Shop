using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Customer.Dtos;


namespace PrimeShop.Application.features.Customer.Commands
{
    internal class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {

        private readonly IRepository<Domain.Entities.Customer> _customerRepo;

        public DeleteCustomerHandler(IRepository<Domain.Entities.Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepo.GetAsync(request.id);

            if (customer == null)
            {
                throw new Exception();
            }


            _customerRepo.Delete(customer);

            await _customerRepo.UnitOfWork.SaveChangesAsync();

            return true;



        }
    }
}