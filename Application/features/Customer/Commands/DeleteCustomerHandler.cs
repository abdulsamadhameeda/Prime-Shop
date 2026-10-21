using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Customer.Dtos;


namespace PrimeShop.Application.features.Customer.Commands
{
    internal class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {

        private readonly IUserService _userService;

        public DeleteCustomerHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var deactivated = await _userService.DeactivateAsync(request.Id);

            if (!deactivated)
            {
                throw new KeyNotFoundException($"User with id {request.Id} was not found.");
            }

            return true;
        }
    }
}