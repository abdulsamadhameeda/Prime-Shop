using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Auth.Register.Dtos;



namespace PrimeShop.Application.features.Auth.Register.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
    {
        private readonly IAuthService _authService;
        private readonly IRepository<Domain.Entities.Customer> _customerRepo;

        public RegisterCommandHandler(IAuthService authService, IRepository<Domain.Entities.Customer> customerRepo)
        {
            _authService = authService;
            _customerRepo = customerRepo;
        }

        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var userId = await _authService.RegisterAsync(request.Email, request.Password);

            var customer = new Domain.Entities.Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                ApplicationUserId = userId
            };

            _customerRepo.Add(customer);
            await _customerRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
