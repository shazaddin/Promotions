using HotFoodStore.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace HotFoodStore.UnitTests.Domain
{
    [TestClass]
    public class SingleProductPromotionTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Value cannot be null")]
        public void When_ApplyingPromotionToNullMenuItem_ShouldThrowException()
        {
            SingleProductPromotion singleProductPromotion = new SingleProductPromotion(1, null, 1.00);

            singleProductPromotion.ApplyPromotion(new List<MenuItem>() { null });
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "'Specified argument was out of the range of valid values")]
        public void When_ApplyingPromotionToMenuItemTheQuantityMustBePositive_ShouldThrowException()
        {
            var margeritaPizza = CreateMenuItem(1, "Margerita pizz", "cheese and tomato", 1.50);
            SingleProductPromotion singleProductPromotion = new SingleProductPromotion(-1, margeritaPizza, 1.00);

            singleProductPromotion.ApplyPromotion(new List<MenuItem>() { margeritaPizza });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "'Specified argument was out of the range of valid values")]
        public void When_ApplyingPromotionToMenuItemThePriceMustBeGreaterThanZero_ShouldThrowException()
        {
            var margeritaPizza = CreateMenuItem(1, "Margerita pizz", "cheese and tomato", 1.50);
            SingleProductPromotion singleProductPromotion = new SingleProductPromotion(2, margeritaPizza, 0.00);

            singleProductPromotion.ApplyPromotion(new List<MenuItem>() { margeritaPizza });
        }

        private static MenuItem CreateMenuItem(int sku, string name, string description, double price)
        {
            return new MenuItem() { Sku = sku, Name = name, Description = description, Price = price };
        }
    }
}
