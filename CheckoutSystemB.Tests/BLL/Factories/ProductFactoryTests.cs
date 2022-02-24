using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckoutSystemB.BLL.Factories.Tests
{
    [TestClass()]
    public class ProductFactoryTests
    {
        [TestMethod()]
        public void CreateProductTest()
        {
            //Arrange
            var fixture = new Fixture();
            decimal id = fixture.Build<decimal>().Create();
            string name = fixture.Build<string>().Create();
            decimal price = fixture.Build<decimal>().Create();
            var bottleFactory = new ProductFactory(id, name, price);
            //Act
            var product = bottleFactory.CreateProduct();
            //Test
            Assert.AreEqual(id, product.Id);
            Assert.AreEqual(name, product.Name);
            Assert.AreEqual(price, product.Price);
        }
    }
}