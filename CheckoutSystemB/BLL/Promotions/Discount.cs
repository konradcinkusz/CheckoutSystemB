using CheckoutSystemB.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutSystemB.BLL.Promotions
{
    public class Discount : Promotion
    {
        private readonly int priceToApply;
        private readonly decimal discountPercentage;

        public Discount(int priceToApply = 75, decimal discountPercentage = 10)
        {
            this.priceToApply = priceToApply;
            this.discountPercentage = discountPercentage;
        }
        public override bool Apply(List<Product> products)
        {
            bool applied = products.Select(x => x.Price).Sum() > priceToApply;
            if (applied)
                products.ForEach(x => x.Price -= x.Price * 0.01M * discountPercentage);
            return applied;
        }
    }
}
