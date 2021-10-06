using HotFoodStore.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace HotFoodStore.UnitTests.Domain
{
    [TestClass]
    public class MultiProductPromotionTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Value cannot be null")]
        public void When_ApplyingPromotionToNullMenuItem_ShouldThrowException()
        {
            MultiProductPromotion multiProductPromotion = new MultiProductPromotion(null, null, 1.00);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Specified argument was out of the range of valid values")]
        public void When_ApplyingPromotionToMenuItemThePriceMustBeGreaterThanZero_ShouldThrowException()
        {
            var margeritaPizza = CreateMenuItem(1, "Margerita pizza", "cheese and tomato", 1.50);
            MultiProductPromotion multiProductPromotion = new MultiProductPromotion(margeritaPizza, margeritaPizza, 0.00);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The SKU for both menu items must be different and positive")]
        public void When_ApplyingPromotionTheMenuItemsSkuMustNotMatch_ShouldThrowException()
        {
            var margeritaPizza = CreateMenuItem(1, "Margerita pizza", "cheese and tomato", 1.50);
            MultiProductPromotion multiProductPromotion = new MultiProductPromotion(margeritaPizza, margeritaPizza, 1.00);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The SKU for both menu items must be different and positive")]
        public void When_ApplyingPromotionTheMenuItemsSkuMustBePositive_ShouldThrowException()
        {
            var margeritaPizza = CreateMenuItem(0, "Margerita pizza", "cheese and tomato", 1.50);
            MultiProductPromotion multiProductPromotion = new MultiProductPromotion(margeritaPizza, margeritaPizza, 1.00);
        }


        private static MenuItem CreateMenuItem(int sku, string name, string description, double price)
        {
            return new MenuItem() { Sku = sku, Name = name, Description = description, Price = price };
        }
    }
}
