using PrimeShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrimeShop.Domain.Entities
{
    public class Category :Entity
    {


        public string Name { get; set; }

        public string Description { get; set; }



    }
}
