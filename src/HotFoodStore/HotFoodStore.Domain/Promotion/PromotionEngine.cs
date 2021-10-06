using HotFoodStore.Domain.Entities;
using System;
using System.Collections.Generic;

namespace HotFoodStore.Domain.Promotion
{
    public class PromotionEngine
    {
        private readonly List<IPromotion> activePromotions;

        public PromotionEngine(List<IPromotion> activePromotions)
        {
            if (activePromotions == null)
            {
                throw new ArgumentNullException(nameof(activePromotions));
            }

            this.activePromotions = activePromotions;
        }

        public void CalculateDiscounts(List<MenuItem> itemsToDiscount)
        {
            foreach (IPromotion promo in activePromotions)
            {
                promo.ApplyPromotion(itemsToDiscount); 
            }
        }
    }
}
