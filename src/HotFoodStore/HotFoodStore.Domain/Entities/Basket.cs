using HotFoodStore.Domain.Promotion;
using System;
using System.Collections.Generic;

namespace HotFoodStore.Domain.Entities
{
    public class Basket
    {
        List<MenuItem> products = new List<MenuItem>();
        private readonly PromotionEngine promotionEngine;

        public List<MenuItem> Products { get => products; set => products = value; }

        public Basket(PromotionEngine engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            this.promotionEngine = engine;
        }

        public void Add(MenuItem item)
        {
            Products.Add(item);
        }

        public void Clear()
        {
            Products.Clear(); 
        }

        public double CalculateTotalWithPromotions()
        {
            double total = 0;
            promotionEngine.CalculateDiscounts(this.Products);

            foreach (MenuItem prod in this.Products)
            {
                if (prod.DiscountedPrice > 0)
                {
                    total += prod.DiscountedPrice;
                }
                else
                {
                    total += prod.Price;
                }
            }

            return total;
        }
    }
}
