using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<bool> UpdateProfileAsync(string userId, string firstName, string lastName, string email, string? phoneNumber);
        Task<bool> DeactivateAsync(string userId);

    }
}
