using CheckoutSystemB.BLL.Promotions;
using CheckoutSystemB.BLL.Services;
using CheckoutSystemB.Shared.Interfaces;
using CheckoutSystemB.Shared.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace CheckoutSystemB.Services.Tests
{
    [TestClass()]
    public class CheckoutServiceTests
    {
        [TestMethod()]
        public void ScanTest()
        {
            ICheckoutService checkout = new CheckoutService();
            bool scanned = checkout.Scan(new List<Product>() { new Product(), new Product(), new Product() });
            Assert.IsTrue(scanned);
        }

        [TestMethod()]
        public void ApplyPromotionsTest()
        {
            ICheckoutService checkout = new CheckoutService();
            bool applied = checkout.ApplyPromotions(ImmutableArray.Create(new Promotion[] { new Discount(), new BottlePriceDrop(0001M) }));
            Assert.IsFalse(applied);
        }

        [TestMethod()]
        public void TestCase1()
        {
            //Arrange
            ICheckoutService checkoutService = new CheckoutService();
            var promotions = ImmutableArray.Create(new Promotion[] { new Discount(), new BottlePriceDrop(0001) });
            checkoutService.Scan(new List<Product>()
            {
                new Product{Id = 0001, Name= "Watter Bottle", Price = 24.95M},
                new Product{Id = 0001, Name= "Watter Bottle", Price = 24.95M},
                new Product{Id = 0002, Name= "Hoodie", Price = 65.00M},
                new Product{Id = 0003, Name= "Sticker Set", Price =  3.99M},
            });
            checkoutService.ApplyPromotions(promotions);
            //Act
            var result = checkoutService.Total();
            //Test
            Assert.IsTrue(decimal.TryParse(result.Split('£')[1], out decimal resultPrice));
            Assert.AreEqual(108.07M, Math.Round(resultPrice, 2));
        }

        [TestMethod()]
        public void TestCase2()
        {
            //Arrange
            ICheckoutService checkoutService = new CheckoutService();
            var promotions = ImmutableArray.Create(new Promotion[] { new Discount(), new BottlePriceDrop(0001) });
            checkoutService.Scan(new List<Product>()
            {
                new Product{Id = 0001, Name= "Watter Bottle", Price = 24.95M},
                new Product{Id = 0001, Name= "Watter Bottle", Price = 24.95M},
                new Product{Id = 0001, Name= "Watter Bottle", Price = 24.95M},
            });
            checkoutService.ApplyPromotions(promotions);
            //Act
            var result = checkoutService.Total();
            //Test
            Assert.IsTrue(decimal.TryParse(result.Split('£')[1], out decimal resultPrice));
            Assert.AreEqual(68.97M, Math.Round(resultPrice, 2));
        }

        [TestMethod()]
        public void TestCase3()
        {
            //Arrange
            ICheckoutService checkoutService = new CheckoutService();
            var promotions = ImmutableArray.Create(new Promotion[] { new Discount(), new BottlePriceDrop(0001) });
            checkoutService.Scan(new List<Product>()
            {
                new Product{Id = 0002, Name= "Hoodie", Price = 65.00M},
                new Product{Id = 0002, Name= "Hoodie", Price = 65.00M},
                new Product{Id = 0003, Name= "Sticker Set", Price =  3.99M},
            });
            checkoutService.ApplyPromotions(promotions);
            //Act
            var result = checkoutService.Total();
            //Test
            Assert.IsTrue(decimal.TryParse(result.Split('£')[1], out decimal resultPrice));
            Assert.AreEqual(120.59M, Math.Round(resultPrice, 2));
        }

        [TestMethod()]
        public void TestCase4()
        {
            //Arrange
            ICheckoutService checkoutService = new CheckoutService();
            var promotions = ImmutableArray.Create(new Promotion[] { new Discount(), new BottlePriceDrop(0001) });
            checkoutService.Scan(new List<Product>()
            {
                new Product{Id = 0002, Name= "Hoodie", Price = 20.00M},
                new Product{Id = 0002, Name= "Hoodie", Price = 20.00M},
                new Product{Id = 0003, Name= "Sticker Set", Price =  3.99M},
            });
            checkoutService.ApplyPromotions(promotions);
            //Act
            var result = checkoutService.Total();
            //Test
            Assert.IsTrue(decimal.TryParse(result.Split('£')[1], out decimal resultPrice));
            Assert.AreEqual(43.99M, Math.Round(resultPrice, 2));
        }
    }
}