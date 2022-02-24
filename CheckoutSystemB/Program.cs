using CheckoutSystemB.BLL.Factories;
using CheckoutSystemB.BLL.Promotions;
using CheckoutSystemB.BLL.Services;
using CheckoutSystemB.Shared.Interfaces;
using CheckoutSystemB.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace CheckoutSystemB
{
    class Program
    {
        static void Main(string[] args)
        {
            //I don't use DI because it is to small project, I 'inject' services manually to interfaces
            ICheckoutService checkoutService = new CheckoutService();

            var promotions = ImmutableArray.Create(new Promotion[] { new Discount(), new BottlePriceDrop(0001) });

            var wBottle = new ProductFactory(0001, "Watter Bottle", 24.95M);
            var hoodie = new ProductFactory(0002, "Hoodie", 65.00M);
            var stickerSet = new ProductFactory(0003, "Sticker Set ", 3.99M);


            Console.WriteLine("Items: 0001,0001,0001");
            checkoutService.Scan(new List<Product>()
            {
                wBottle.CreateProduct(),
                wBottle.CreateProduct(),
                hoodie.CreateProduct(),
                stickerSet.CreateProduct()
            });
            checkoutService.ApplyPromotions(promotions);
            Console.WriteLine(checkoutService.Total());

            Console.WriteLine("Items: 0001,0001,0001");
            checkoutService.Scan(new List<Product>()
            {
                wBottle.CreateProduct(),
                wBottle.CreateProduct(),
                wBottle.CreateProduct()
            });
            checkoutService.ApplyPromotions(promotions);
            Console.WriteLine(checkoutService.Total());

            Console.WriteLine("Items: 0002,0002,0003");
            checkoutService.Scan(new List<Product>()
            {
                hoodie.CreateProduct(),
                hoodie.CreateProduct(),
                stickerSet.CreateProduct()
            });
            checkoutService.ApplyPromotions(promotions);
            Console.WriteLine(checkoutService.Total());
        }
    }
}
