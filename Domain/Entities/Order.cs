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


        public DateTime OrderDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

       
        public OrderStatus Status { get; set; }

      
        public string ShippingStreet { get; set; }


        [Required]
        [ForeignKey("ShippingCityLookup")]
        public Guid ShippingCityId { get; set; }
        public Lookup ShippingCityLookup { get; set; }

        [Required]
        [ForeignKey("Customer")]

        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }
       

    }
}
