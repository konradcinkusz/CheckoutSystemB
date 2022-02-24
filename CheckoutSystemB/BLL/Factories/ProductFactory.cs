using CheckoutSystemB.Shared.Models;

namespace CheckoutSystemB.BLL.Factories
{
    public class ProductFactory
    {
        private readonly decimal id;
        private readonly string name;
        private readonly decimal price;

        public ProductFactory(decimal id, string name, decimal price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }
        public Product CreateProduct() => new Product { Id = id, Name = name, Price = price };
    }
}
