using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PrimeShop.Domain.Entities
{
    public class OrderItem
    {

        [Required]
        public Guid Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }

        [Required]
        [ForeignKey("Order")]

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        [Required]
        [ForeignKey("Product")]

        public Guid ProductId { get; set; }

        public Product Product { get; set; }


    }
}
