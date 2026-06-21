using PrimeShop.Domain.Common;
using PrimeShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PrimeShop.Domain.Entities
{
    public class Order :Entity
    {


        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        [MaxLength(100)]
        public string ShippingStreet { get; set; }

        [Required]
        [MaxLength(50)]
        public string ShippingCity { get; set; }


        [Required]
        [ForeignKey("Customer")]

        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }
       

    }
}
