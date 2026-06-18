using PrimeShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PrimeShop.Domain.Entities
{
    public class BasketItem :Entity
    {
        public Guid Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [ForeignKey("Basket")]

        public Guid BasketId { get; set; }

        public Basket Basket { get; set; }


        [Required]
        [ForeignKey("Product")]

        public Guid ProductId { get; set; }

        public Product Product { get; set; }

       


    }
}
