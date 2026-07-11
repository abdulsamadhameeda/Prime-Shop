using PrimeShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PrimeShop.Domain.Entities
{
    public class OrderItem : Entity
    {

    

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }

        [ForeignKey("Order")]

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        [ForeignKey("Product")]

        public Guid ProductId { get; set; }

        public Product Product { get; set; }


    }
}
