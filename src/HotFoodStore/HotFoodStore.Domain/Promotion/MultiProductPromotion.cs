using HotFoodStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotFoodStore.Domain.Promotion
{
    public class MultiProductPromotion : IPromotion
    {
        private const int PRODUCTQUANTITY = 2;
        private readonly MenuItem menuItemToDiscount1;
        private readonly MenuItem menuItemToDiscount2;
        private readonly double discountPrice;

        public string Name { get; set; }

        public MultiProductPromotion(MenuItem menuItemToDiscount1, MenuItem menuItemToDiscount2, double discountPrice)
        {
            if (menuItemToDiscount1 == null)
            {
                throw new ArgumentNullException(nameof(menuItemToDiscount1));
            }

            if (menuItemToDiscount2 == null)
            {
                throw new ArgumentNullException(nameof(menuItemToDiscount2));
            }

            if (discountPrice <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(discountPrice));
            }

            if (menuItemToDiscount1.Sku <= 0 || menuItemToDiscount2.Sku <= 0 || menuItemToDiscount1.Sku == menuItemToDiscount2.Sku)
            {
                throw new ArgumentException("The SKU for both menu items must be different and positive");
            }

            this.menuItemToDiscount1 = menuItemToDiscount1;
            this.menuItemToDiscount2 = menuItemToDiscount2;
            this.discountPrice = discountPrice;
        }

        public void ApplyPromotion(List<MenuItem> allOrderedItems)
        {
            var matchingItemsThatCanBeDiscounted1 = allOrderedItems.Where(p => (p.Sku == this.menuItemToDiscount1.Sku && p.DiscountedPrice == 0)).ToList();
            var matchingItemsThatCanBeDiscounted2 = allOrderedItems.Where(p => (p.Sku == this.menuItemToDiscount2.Sku && p.DiscountedPrice == 0)).ToList();

            int minimumMatchPairs = Math.Min(matchingItemsThatCanBeDiscounted1.Count(), matchingItemsThatCanBeDiscounted2.Count());

            matchingItemsThatCanBeDiscounted1.Take(minimumMatchPairs).ToList().ForEach(p => p.DiscountedPrice = discountPrice / PRODUCTQUANTITY);
            matchingItemsThatCanBeDiscounted2.Take(minimumMatchPairs).ToList().ForEach(p => p.DiscountedPrice = discountPrice / PRODUCTQUANTITY);
            matchingItemsThatCanBeDiscounted1.Take(minimumMatchPairs).ToList().ForEach(p => p.PromotionText = Name);
            matchingItemsThatCanBeDiscounted2.Take(minimumMatchPairs).ToList().ForEach(p => p.PromotionText = Name);
        }
    }
}