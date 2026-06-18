using PrimeShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PrimeShop.Domain.Entities
{
    public class Basket : Entity
    {


        public Guid Id  { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }


        [Required]
        [ForeignKey("Customer")]

        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }
        

    }
}
