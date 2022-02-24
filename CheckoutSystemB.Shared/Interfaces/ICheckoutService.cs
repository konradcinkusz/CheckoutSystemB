using CheckoutSystemB.Shared.Models;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace CheckoutSystemB.Shared.Interfaces
{
    public interface ICheckoutService
    {
        bool ApplyPromotions(ImmutableArray<Promotion> promotions);
        bool Scan(List<Product> products);
        string Total();
    }
}