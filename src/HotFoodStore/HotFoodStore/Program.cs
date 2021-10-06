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
            // SKUS
            var margerita = CreateNewProduct(1, "Margerita", "pizza", 50.00);
            var vegetarian = CreateNewProduct(2, "Vegetarian", "pizza", 30.00);
            var cheeseburger = CreateNewProduct(3, "Cheese", "Cheese", 20.00);
            var towerBurger = CreateNewProduct(4, "tower", "burger", 15.00);

            //ACTIVE PROMOTIONS
            List<IPromotion> promotions = new List<IPromotion>();
            promotions.Add(new SingleProductPromotion(3, margerita, 130.00) { Name = "3 Margeritas for £130.00" });
            promotions.Add(new SingleProductPromotion(2, vegetarian, 45.00) { Name = "2 Vegetarians for £45.00" });
            promotions.Add(new MultiProductPromotion(cheeseburger, towerBurger, 30.00) { Name = "Cheese & Tower for £30.00" });
            PromotionEngine engine = new PromotionEngine(promotions);
            
            // SCENARIO A
            Basket basket = new Basket(engine);

            basket.Add(CreateNewProduct(1, "Margerita", "pizza", 50.00)); 
            basket.Add(CreateNewProduct(2, "vegetarian", "pizza", 30.00)); 
            basket.Add(CreateNewProduct(3, "Cheese", "Cheese", 20.00));

            double orderTotal = basket.CalculateTotalWithPromotions();

            Console.WriteLine("----------SCENARIO A-------------");

            foreach (MenuItem item in basket.Products)
            {
                Console.WriteLine("{0}---{1}---Price={2:C}---Discounted Price={3:C}---{4}", item.Sku, item.Name, item.Price, item.DiscountedPrice, item.PromotionText); 
            }

            Console.WriteLine("Total to Pay - {0:C}", orderTotal);

            // SCENARIO B
            basket.Clear();

            basket.Add(CreateNewProduct(1, "Margerita", "pizza", 50.00));
            basket.Add(CreateNewProduct(1, "Margerita", "pizza", 50.00));
            basket.Add(CreateNewProduct(1, "Margerita", "pizza", 50.00));
            basket.Add(CreateNewProduct(1, "Margerita", "pizza", 50.00));
            basket.Add(CreateNewProduct(1, "Margerita", "pizza", 50.00));

            basket.Add(CreateNewProduct(2, "vegetarian", "pizza", 30.00));
            basket.Add(CreateNewProduct(2, "vegetarian", "pizza", 30.00));
            basket.Add(CreateNewProduct(2, "vegetarian", "pizza", 30.00));
            basket.Add(CreateNewProduct(2, "vegetarian", "pizza", 30.00));
            basket.Add(CreateNewProduct(2, "vegetarian", "pizza", 30.00));

            basket.Add(CreateNewProduct(3, "Cheese", "Cheese", 20.00));

            orderTotal = basket.CalculateTotalWithPromotions();

            Console.WriteLine("----------SCENARIO B-------------");

            foreach (MenuItem item in basket.Products)
            {
                Console.WriteLine("{0}---{1}---Price={2:C}---Discounted Price={3:C}---{4}", item.Sku, item.Name, item.Price, item.DiscountedPrice, item.PromotionText);
            }

            Console.WriteLine("Total to Pay - {0:C}", orderTotal);

        }

        private static MenuItem CreateNewProduct(int sku, string name, string description, double price)
        {
            return new MenuItem() { Sku = sku, Name = name, Description = description, Price = price };
        }
    }
}
