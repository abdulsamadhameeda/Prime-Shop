using PrimeShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PrimeShop.Domain.Entities
{
    public class Customer : Entity
    {



      
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

       
        public string PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public string? ApplicationUserId { get; set; } 


    }
}
