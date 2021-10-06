using FluentAssertions;
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
            var margeritaPizza = CreateMenuItem(1, "Margerita pizza", "cheese and tomato", 1.50);
            SingleProductPromotion singleProductPromotion = new SingleProductPromotion(-1, margeritaPizza, 1.00);

            singleProductPromotion.ApplyPromotion(new List<MenuItem>() { margeritaPizza });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "'Specified argument was out of the range of valid values")]
        public void When_ApplyingPromotionToMenuItemThePriceMustBeGreaterThanZero_ShouldThrowException()
        {
            var margeritaPizza = CreateMenuItem(1, "Margerita pizza", "cheese and tomato", 1.50);
            SingleProductPromotion singleProductPromotion = new SingleProductPromotion(2, margeritaPizza, 0.00);

            singleProductPromotion.ApplyPromotion(new List<MenuItem>() { margeritaPizza });
        }

        [TestMethod]
        public void When_ApplyingPromotionToOneMenuItem_ShouldNotRecieveDiscount()
        {
            var margeritaPizza = CreateMenuItem(1, "Margerita pizza", "cheese and tomato", 1.50);
            SingleProductPromotion singleProductPromotion = new SingleProductPromotion(2, margeritaPizza, 1.00);

            var orderedItems = new List<MenuItem>() { margeritaPizza }; 
            singleProductPromotion.ApplyPromotion(orderedItems);

            orderedItems[0].Price.Should().Be(1.50);
            orderedItems[0].DiscountedPrice.Should().Be(0); 
        }

        [TestMethod]
        public void When_ApplyingPromotionTwoDifferentMenuItems_ShouldNotRecieveDiscount()
        {
            var margeritaPizza = CreateMenuItem(1, "Margerita pizza", "cheese and tomato", 1.50);
            var VegetarianPizza = CreateMenuItem(2, "Vegetarian pizza", "cheese, tomato, onions & peppers", 2.00);

            SingleProductPromotion singleProductPromotion = new SingleProductPromotion(2, margeritaPizza, 1.00);

            var orderedItems = new List<MenuItem>() { margeritaPizza, VegetarianPizza };
            singleProductPromotion.ApplyPromotion(orderedItems);

            orderedItems[0].Price.Should().Be(1.50);
            orderedItems[0].DiscountedPrice.Should().Be(0);

            orderedItems[1].Price.Should().Be(2.00);
            orderedItems[1].DiscountedPrice.Should().Be(0);
        }

        [TestMethod]
        public void When_ApplyingPromotionTwoOfTheSameMenuItems_ShouldRecieveDiscount()
        {
            var margeritaPizza = CreateMenuItem(1, "Margerita pizza", "cheese and tomato", 1.50);
            var VegetarianPizza = CreateMenuItem(2, "Vegetarian pizza", "cheese, tomato, onions & peppers", 2.00);

            SingleProductPromotion singleProductPromotion = new SingleProductPromotion(2, margeritaPizza, 1.00);

            var orderedItems = new List<MenuItem>() { margeritaPizza, margeritaPizza, VegetarianPizza };
            singleProductPromotion.ApplyPromotion(orderedItems);

            orderedItems[0].Price.Should().Be(1.50);
            orderedItems[0].DiscountedPrice.Should().Be(0.50);

            orderedItems[1].Price.Should().Be(1.50);
            orderedItems[1].DiscountedPrice.Should().Be(0.50);

            orderedItems[2].Price.Should().Be(2.00);
            orderedItems[2].DiscountedPrice.Should().Be(0);
        }

        private static MenuItem CreateMenuItem(int sku, string name, string description, double price)
        {
            return new MenuItem() { Sku = sku, Name = name, Description = description, Price = price };
        }
    }
}
