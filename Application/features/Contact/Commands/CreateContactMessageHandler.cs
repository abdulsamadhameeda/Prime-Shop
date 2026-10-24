using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Contact.Dtos;
using PrimeShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace PrimeShop.Application.features.Contact.Commands
{
    internal class CreateContactMessageHandler : IRequestHandler<CreateContactMessageCommand, Guid>
    {
        private readonly IRepository<ContactMessage> _contactMessageRepo;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateContactMessageHandler> _logger;

        public CreateContactMessageHandler(
            IRepository<ContactMessage> contactMessageRepo,
            IEmailService emailService,
            ILogger<CreateContactMessageHandler> logger)
        {
            _contactMessageRepo = contactMessageRepo;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateContactMessageCommand request,
            CancellationToken cancellationToken)
        {
            var contactMessage = new ContactMessage
            {
                Name = request.Name,
                Email = request.Email,
                Subject = request.Subject,
                Message = request.Message,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            _contactMessageRepo.Add(contactMessage);

            await _contactMessageRepo.UnitOfWork.SaveChangesAsync(cancellationToken);


            try
            {
                await _emailService.SendContactNotificationAsync(
                    contactMessage,
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to send contact message notification for {ContactMessageId}",
                    contactMessage.Id);
            }


            return contactMessage.Id;   
        }
    }
}
