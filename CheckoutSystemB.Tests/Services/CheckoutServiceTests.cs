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

        const decimal TestCase1_expected_result = 108.07M;

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
            Assert.AreEqual(TestCase1_expected_result, Math.Round(resultPrice, 2));
        }

        /// <summary>
        /// We expect the same result as in TestCase1
        /// With this test we can see that the analytic document isn’t well enough because there is no information 
        /// about promotion priority, and we should (nice to have) implement some logic of promotions ordering, 
        /// which promotions should be applied as first. With this solution ordering is resolved by just adding 
        /// promotions in the right order. I implement better solution that in my 
        /// lack-of-tests-unstructured .NET 6 C# 10 solution.
        /// https://github.com/konradcinkusz/CheckoutSystem/blob/feature/promotions_ordering/CheckoutSystem/Program.cs
        /// </summary>
        [TestMethod()]
        public void TestCase1_a_failed()
        {
            //Arrange
            ICheckoutService checkoutService = new CheckoutService();
            var promotions = ImmutableArray.Create(new Promotion[] { new BottlePriceDrop(0001), new Discount() });
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
            Assert.AreEqual(TestCase1_expected_result, Math.Round(resultPrice, 2));
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