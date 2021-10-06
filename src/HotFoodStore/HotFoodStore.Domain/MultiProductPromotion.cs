using System;
using System.Collections.Generic;
using System.Linq;

namespace HotFoodStore.Domain
{
    public class MultiProductPromotion
    {
        private readonly MenuItem menuItemToDiscount1;
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

            if (menuItemToDiscount1.Sku <= 0|| menuItemToDiscount2.Sku <= 0 || menuItemToDiscount1.Sku == menuItemToDiscount2.Sku)
            {
                throw new ArgumentException("The SKU for both menu items must be different and positive");
            }

            this.menuItemToDiscount1 = menuItemToDiscount1;
            this.discountPrice = discountPrice;
        }

        public void ApplyPromotion(List<MenuItem> allOrderedItems)
        {

        }
    }
}