using HotFoodStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotFoodStore.Domain.Promotion
{
    public class SingleProductPromotion : IPromotion
    {
        private readonly MenuItem menuItemToDiscount;
        private readonly int discountQuantity;
        private readonly double discountPrice;

        public string Name { get; set; }


        public SingleProductPromotion(int discountQuantity, MenuItem menuItemToDiscount, double discountPrice)
        {
            if (menuItemToDiscount == null)
            {
                throw new ArgumentNullException(nameof(menuItemToDiscount));
            }

            if (discountQuantity <= 0 || discountPrice <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(discountQuantity));
            }

            if (discountPrice <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(discountPrice));
            }

            this.menuItemToDiscount = menuItemToDiscount;
            this.discountQuantity = discountQuantity;
            this.discountPrice = discountPrice;
        }

        public void ApplyPromotion(List<MenuItem> allOrderedItems)
        {
            var matchingItemsThatCanBeDiscounted = allOrderedItems.Where(p => p.Sku == menuItemToDiscount.Sku && p.DiscountedPrice == 0).ToList();
            var mod = matchingItemsThatCanBeDiscounted.Count % discountQuantity;

            var productsToDiscount = matchingItemsThatCanBeDiscounted.Skip(mod).ToList();
            productsToDiscount.ForEach(p => p.DiscountedPrice = discountPrice / discountQuantity);
        }
    }
}