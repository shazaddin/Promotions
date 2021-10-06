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
            promotions.Add(new SingleProductPromotion(2, CreateNewProduct(1, "Margerita", "pizza", 0.75), 1.00) { Name = "2 for £1.00" });
            promotions.Add(new MultiProductPromotion(CreateNewProduct(3, "Cheese", "burger", 1.00), CreateNewProduct(4, "tower", "burger", 1.00), 1.50) { Name = "Cheese & Tower burger Combo for £1.50" });
            PromotionEngine engine = new PromotionEngine(promotions);
            
            Basket basket = new Basket(engine);

            basket.Add(CreateNewProduct(1, "Margerita", "pizza", 0.75)); // full price
            basket.Add(CreateNewProduct(2, "vegetarian", "pizza", 1.00)); // full price
            basket.Add(CreateNewProduct(1, "Margerita", "pizza", 0.75)); // discounted
            basket.Add(CreateNewProduct(1, "Margerita", "pizza", 0.75)); // discounted

            double orderTotal = basket.CalculateTotalWithPromotions();

            foreach (MenuItem item in basket.Products)
            {
                Console.WriteLine("{0}---{1}---{2}---{3:C}---{4:C}", item.Sku, item.Name, item.Price, item.DiscountedPrice, item.PromotionText); 
            }

            Console.WriteLine("Total to Pay - {0:C}", orderTotal);
        }

        private static MenuItem CreateNewProduct(int sku, string name, string description, double price)
        {
            return new MenuItem() { Sku = sku, Name = name, Description = description, Price = price };
        }
    }
}
