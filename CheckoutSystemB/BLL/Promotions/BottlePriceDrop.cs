using CheckoutSystemB.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutSystemB.BLL.Promotions
{
    public class BottlePriceDrop : Promotion
    {
        private readonly decimal waterBottleId;
        private readonly int wBottCount;
        private readonly decimal newWaterPrice;

        public BottlePriceDrop(decimal waterBottleId, int wBottCount = 2, decimal newWaterPrice = 22.99M)
        {
            this.waterBottleId = waterBottleId;
            this.wBottCount = wBottCount;
            this.newWaterPrice = newWaterPrice;
        }
        public override bool Apply(List<Product> products)
        {
            var wBott = products.Where(x => x.Id == waterBottleId).ToList();
            bool applied = wBott.Count >= wBottCount;
            if (applied)
            {
                wBott.ForEach(x => x.Price = newWaterPrice);
                applied = true;
            }
            return applied;
        }
    }
}
