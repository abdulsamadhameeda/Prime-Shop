using PrimeShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Domain.Entities
{
    public class Lookup : Entity
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
