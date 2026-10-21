using Microsoft.AspNetCore.Identity;
using PrimeShop.Application.Common.Interfaces;


namespace PrimeShop.Infrastructure.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtTokenService _jwtTokenService;


        public AuthService(UserManager<ApplicationUser> userManager , JwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<string> RegisterAsync(string email, string password, string firstName, string lastName, string? phoneNumber)
        {
            var user = new ApplicationUser
            {
                Email = email,
                UserName = email,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(user, "Customer");

            return user.Id;
        }
        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || user.IsDeleted) return null;

            var result = await _userManager.CheckPasswordAsync(user, password);

            if (!result) return null;

            var roles = await _userManager.GetRolesAsync(user);


            return _jwtTokenService.GenerateToken(user, roles.First());
        }
    }
}
