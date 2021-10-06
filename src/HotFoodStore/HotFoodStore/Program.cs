using HotFoodStore.Domain.Entities;
using HotFoodStore.Domain.Promotion;
using System;
using System.Collections.Generic;

namespace HotFoodStore
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IPromotion> promotions = new List<IPromotion>();
            promotions.Add(new SingleProductPromotion(2, CreateNewProduct(1, "Margerita", "pizza", 0.75), 1.00));
            promotions.Add(new MultiProductPromotion(CreateNewProduct(3, "Cheese", "burger", 1.00), CreateNewProduct(4, "tower", "burger", 1.00), 1.50));

            List<MenuItem> orderItems = new List<MenuItem>();
            orderItems.Add(CreateNewProduct(1, "Margerita", "pizza", 0.75)); // full price
            orderItems.Add(CreateNewProduct(2, "vegetarian", "pizza", 1.00)); // full price
            orderItems.Add(CreateNewProduct(1, "Margerita", "pizza", 0.75)); // discounted
            orderItems.Add(CreateNewProduct(1, "Margerita", "pizza", 0.75)); // discounted

            PromotionEngine engine = new PromotionEngine(promotions);
            engine.CalculateDiscounts(orderItems);

            foreach (MenuItem item in orderItems)
            {
                Console.WriteLine("{0}-{1}-{2}-{3}", item.Sku, item.Name, item.Price, item.DiscountedPrice); 
            }
        }

        private static MenuItem CreateNewProduct(int sku, string name, string description, double price)
        {
            return new MenuItem() { Sku = sku, Name = name, Description = description, Price = price };
        }
    }
}
