using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AutoFixture;
using System.Linq;
using CheckoutSystemB.Shared.Models;

namespace CheckoutSystemB.BLL.Promotions.Tests
{
    [TestClass()]
    public class BottlePriceDropTests
    {
        [TestMethod()]
        public void ApplyTest()
        {
            //Arrange
            var fixture = new Fixture();
            var intGen = fixture.Create<Generator<int>>();
            var decGen = fixture.Create<Generator<decimal>>();
            var productIds = decGen.Where(x => x != 0).Distinct().Take(2).ToList();
            var bottlePrice = decGen.FirstOrDefault(x => x > 0);
            var otherProductPrice = decGen.FirstOrDefault(x => x > bottlePrice);
            var newBottlePrice = decGen.FirstOrDefault(x => x > 0 && x <= bottlePrice);

            var bottle1 = fixture.Build<Product>().With(x => x.Price, bottlePrice).With(x => x.Id, productIds[0]).Create();
            var bottle2 = fixture.Build<Product>().With(x => x.Price, bottlePrice).With(x => x.Id, productIds[0]).Create();
            var bottle3 = fixture.Build<Product>().With(x => x.Price, bottlePrice).With(x => x.Id, productIds[0]).Create();
            var otherProduct = fixture.Build<Product>().With(x => x.Price, otherProductPrice).With(x => x.Id, productIds[1]).Create();

            var bottlePriceDrop = new BottlePriceDrop(productIds[0], 2, newBottlePrice);

            //Act
            bottlePriceDrop.Apply(new List<Product> { bottle1, bottle2, bottle3, otherProduct });

            //Test
            Assert.AreEqual(newBottlePrice, bottle1.Price);
            Assert.AreEqual(newBottlePrice, bottle2.Price);
            Assert.AreEqual(newBottlePrice, bottle3.Price);
            Assert.AreEqual(otherProductPrice, otherProduct.Price);
            Assert.AreNotEqual(newBottlePrice, otherProduct.Price);
        }
    }
}