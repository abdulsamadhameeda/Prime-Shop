using Microsoft.AspNetCore.Identity;
using PrimeShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PrimeShop.Infrastructure.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> UpdateProfileAsync(string userId, string firstName, string lastName, string email, string? phoneNumber)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return false;

            user.FirstName = firstName;
            user.LastName = lastName;
            user.PhoneNumber = phoneNumber;

            if (!string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase))
            {
                var emailResult = await _userManager.SetEmailAsync(user, email);
                if (!emailResult.Succeeded)
                    return false;

                var userNameResult = await _userManager.SetUserNameAsync(user, email);
                if (!userNameResult.Succeeded)
                    return false;
            }

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> DeactivateAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return false;

            user.IsDeleted = true;
            user.DeletedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }
    }
}
