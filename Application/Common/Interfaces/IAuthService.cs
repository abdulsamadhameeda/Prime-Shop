using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(string email, string password , Guid customerId);
        Task<string> LoginAsync(string email, string password);

    }
}
