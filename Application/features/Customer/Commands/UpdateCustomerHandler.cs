using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Customer.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Customer.Commands
{
    internal class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, string>
    {
        private readonly IUserService _userService;

        public UpdateCustomerHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var updated = await _userService.UpdateProfileAsync(
                request.Id,
                request.FirstName,
                request.LastName,
                request.Email,
                request.PhoneNumber);

            if (!updated)
            {
                throw new KeyNotFoundException($"User with id {request.Id} was not found or could not be updated.");
            }

            return request.Id;
        }
    }
}
