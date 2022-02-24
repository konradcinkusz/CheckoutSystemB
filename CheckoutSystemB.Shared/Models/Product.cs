namespace CheckoutSystemB.Shared.Models
{
    public class Product
    {
        public decimal Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public override string ToString() => $"ID: {Id:0000}, Name: {Name}, Price: {Price}";
    }
}
