using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrimeShop.Application.features.Customer.Dtos
{
    internal class CustomerDto
    {
        public Guid Id { get; set; }

      
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

       
        public string PhoneNumber { get; set; }
    }
}
