using CheckoutSystemB.Shared.Interfaces;
using CheckoutSystemB.Shared.Models;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace CheckoutSystemB.BLL.Services
{
    public class CheckoutService : ICheckoutService
    {
        //Eventually, if we don't plan to expand Checkout classes we can use List<Product> directly
        private Checkout checkout { get; set; } = null;
        public bool Scan(List<Product> products)
        {
            bool scanned = checkout == null;
            if (scanned)
                checkout = new Checkout() { Products = products };
            return scanned;
        }
        public string Total()
        {
            string result = "Scan first product, than call total";
            if (checkout != null)
            {
                result = $"Total Price: £{checkout.Products.Select(x => x.Price).Sum().ToString("0.##")}";
                checkout = null;
            }
            return result;
        }
        public bool ApplyPromotions(ImmutableArray<Promotion> promotions)
        {
            List<bool> applied = new List<bool>();
            bool hasCheckout = checkout != null && checkout.Products.Any();
            if (hasCheckout)
                promotions.ToImmutableList().ForEach(x => applied.Add(x.Apply(checkout.Products)));
            return hasCheckout && !applied.Any(x => x == false);
        }
    }
}
