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

        public async Task<string> RegisterAsync(string email, string password ,Guid customerId)
            {
            var user = new ApplicationUser
            {
                Email = email,
                UserName = email,
                CustomerId = customerId

            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new Exception();

            await _userManager.AddToRoleAsync(user, "Customer");

            return user.Id;
        }
        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new Exception("user not found");

            var result = await _userManager.CheckPasswordAsync(user, password);

            if (!result)
                throw new Exception("error password");

            var roles = await _userManager.GetRolesAsync(user);


            return _jwtTokenService.GenerateToken(user, roles.First());
        }
    }
}
