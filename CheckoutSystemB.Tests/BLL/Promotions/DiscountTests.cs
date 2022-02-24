using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AutoFixture;
using System.Linq;
using CheckoutSystemB.Shared.Models;

namespace CheckoutSystemB.BLL.Promotions.Tests
{
    [TestClass()]
    public class DiscountTests
    {
        [TestMethod()]
        public void ApplyTest()
        {
            //Arrange
            var fixture = new Fixture();
            var intGen = fixture.Create<Generator<int>>();
            var decGen = fixture.Create<Generator<decimal>>();
            var priceToApply = intGen.FirstOrDefault(x => x > 0);
            var discountPercentage = decGen.FirstOrDefault(x => x >= 0 && x <= 100);
            var product1 = fixture.Build<Product>().Create();
            var prod1InitialPrice = product1.Price;
            var product2 = fixture.Build<Product>().Create();
            var prod2InitialPrice = product2.Price;
            var product3 = fixture.Build<Product>().Create();
            var prod3InitialPrice = product3.Price;
            var discount = new Discount(priceToApply, discountPercentage);
            //Act
            discount.Apply(new List<Product> { product1, product2, product3 });
            //Test
            var expectedProduct1Price = prod1InitialPrice - prod1InitialPrice * 0.01M * discountPercentage;
            Assert.AreEqual(expectedProduct1Price, product1.Price);
            var expectedProduct2Price = prod2InitialPrice - prod2InitialPrice * 0.01M * discountPercentage;
            Assert.AreEqual(expectedProduct2Price, product2.Price);
            var expectedProduct3Price = prod3InitialPrice - prod3InitialPrice * 0.01M * discountPercentage;
            Assert.AreEqual(expectedProduct3Price, product3.Price);
        }
    }
}