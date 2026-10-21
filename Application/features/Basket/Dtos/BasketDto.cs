using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PrimeShop.Application.features.Basket.Dtos
{
    public class BasketDto
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }

    }
}
