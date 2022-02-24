using AutoFixture;
using CheckoutSystemB.BLL.Promotions;
using CheckoutSystemB.Shared.Interfaces;
using CheckoutSystemB.Shared.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace CheckoutSystemBTests.Services
{

    [TestClass()]
    public class CheckoutServiceTests_Moq
    {
        [TestMethod()]
        public void ScanTest_Moq()
        {
            var checkoutService = new Mock<ICheckoutService>();
            checkoutService.Setup(x => x.Scan(It.IsAny<List<Product>>())).Returns(true);

            bool scanned = checkoutService.Object.Scan(new List<Product>() { new Product(), new Product(), new Product() });
            Assert.IsTrue(scanned);
        }

        [TestMethod()]
        public void ScanTest_Moq2()
        {
            var checkoutService = new Mock<ICheckoutService>();
            checkoutService.SetupSequence(x => x.Scan(It.IsAny<List<Product>>())).Returns(true).Returns(false);
            
            checkoutService.Object.Scan(new List<Product>() { new Product(), new Product(), new Product() });
            bool scanned = checkoutService.Object.Scan(new List<Product>() { new Product(), new Product(), new Product() });
            Assert.IsFalse(scanned);
        }

        [TestMethod()]
        public void ApplyPromotionsTest_Moq()
        {
            var checkoutService = new Mock<ICheckoutService>();
            checkoutService.Setup(x => x.ApplyPromotions(It.IsAny<ImmutableArray<Promotion>>())).Returns(true);

            bool applied = checkoutService.Object.ApplyPromotions(ImmutableArray.Create(new Promotion[] { new Discount(), new BottlePriceDrop(0001M) }));
            Assert.IsTrue(applied);
        }

        [TestMethod()]
        public void ApplyPromotionsTest_Moq2()
        {
            var checkoutService = new Mock<ICheckoutService>();
            var notAppliedPromotion = new Mock<Promotion>();

            notAppliedPromotion.Setup(x => x.Apply(It.IsAny<List<Product>>())).Returns(false);

            checkoutService.Setup(x => x.ApplyPromotions(It.Is<ImmutableArray<Promotion>>(s => s.Contains(notAppliedPromotion.Object)))).Returns(false);

            bool applied = checkoutService.Object.ApplyPromotions(ImmutableArray.Create(new Promotion[] { notAppliedPromotion.Object, new BottlePriceDrop(0001M) }));
            Assert.IsFalse(applied);
        }

        [TestMethod()]
        public void TotalTests_Moq()
        {
            //Arrange
            var checkoutService = new Mock<ICheckoutService>();
            var fixture = new Fixture();
            checkoutService.Setup(x=>x.Total()).Returns(fixture.Build<string>().Create());
            //Test
            Assert.IsFalse(string.IsNullOrEmpty(checkoutService.Object.Total()));
        }
    }
}
