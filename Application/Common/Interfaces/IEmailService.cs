using PrimeShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendContactNotificationAsync(ContactMessage contactMessage, CancellationToken cancellationToken = default);
    }
}
