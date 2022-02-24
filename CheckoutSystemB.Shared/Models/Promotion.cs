using System;
using System.Collections.Generic;

namespace CheckoutSystemB.Shared.Models
{
    public abstract class Promotion
    {
        public long CreateDate { get; set; } = DateTime.Now.Ticks;
        public abstract bool Apply(List<Product> products);
    }
}
