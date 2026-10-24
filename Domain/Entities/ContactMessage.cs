using PrimeShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrimeShop.Domain.Entities
{
    public class ContactMessage : Entity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; } 

        [Required]
        public string Subject { get; set; } 

        [Required]
        public string Message { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsRead { get; set; } = false;   
    }
}
