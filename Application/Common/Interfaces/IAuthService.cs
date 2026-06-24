using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(string email, string password);
        Task<string> LoginAsync(string email, string password);

    }
}
