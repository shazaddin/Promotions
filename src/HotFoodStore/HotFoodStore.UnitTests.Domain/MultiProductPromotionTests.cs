using FluentAssertions;
using HotFoodStore.Domain.Entities;
using HotFoodStore.Domain.Promotion;
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

        [TestMethod]
        public void When_ApplyingPromotionNotQualifyMenuItems_ShouldNotRecieveDiscount()
        {
            var margeritaPizza = CreateMenuItem(1, "Margerita pizza", "cheese and tomato", 1.50);
            var vegetarianPizza = CreateMenuItem(2, "Vegetarian pizza", "cheese, tomato, onions, peppers", 2.00);
            var polloPizza = CreateMenuItem(3, "Pollo pizza", "cheese tomato chicken", 2.50);

            MultiProductPromotion multiProductPromotion = new MultiProductPromotion(margeritaPizza, vegetarianPizza, 1.00);

            var orderedItems = new List<MenuItem>() { margeritaPizza, polloPizza };
            multiProductPromotion.ApplyPromotion(orderedItems);

            margeritaPizza.Price.Should().Be(1.50);
            margeritaPizza.DiscountedPrice.Should().Be(0); //discount not given
            polloPizza.Price.Should().Be(2.50);
            polloPizza.DiscountedPrice.Should().Be(0); //discount not given
        }

        [TestMethod]
        public void When_ApplyingPromotionQualifyMenuItems_ShouldRecieveDiscount()
        {
            var margeritaPizza = CreateMenuItem(1, "Margerita pizza", "cheese and tomato", 1.50);
            var vegetarianPizza = CreateMenuItem(2, "Vegetarian pizza", "cheese, tomato, onions, peppers", 2.00);
            var polloPizza = CreateMenuItem(3, "Pollo pizza", "cheese tomato chicken", 2.50);

            MultiProductPromotion multiProductPromotion = new MultiProductPromotion(margeritaPizza, vegetarianPizza, 1.00);

            var orderedItems = new List<MenuItem>() { margeritaPizza, vegetarianPizza };
            multiProductPromotion.ApplyPromotion(orderedItems);

            margeritaPizza.Price.Should().Be(1.50);
            margeritaPizza.DiscountedPrice.Should().Be(0.50); //discount given
            vegetarianPizza.Price.Should().Be(2.00);
            vegetarianPizza.DiscountedPrice.Should().Be(0.50); //discount given
        }

        [TestMethod]
        public void When_ApplyingPromotionThreeQualifyMenuItems_OnlyTwoItemsShouldRecieveDiscount()
        {
            var margeritaPizza = CreateMenuItem(1, "Margerita pizza", "cheese and tomato", 1.50);
            var vegetarianPizza = CreateMenuItem(2, "Vegetarian pizza", "cheese, tomato, onions, peppers", 2.00);
            var vegetarianPizza1 = CreateMenuItem(2, "Vegetarian pizza", "cheese, tomato, onions, peppers", 2.00);
            var polloPizza = CreateMenuItem(3, "Pollo pizza", "cheese tomato chicken", 2.50);

            MultiProductPromotion multiProductPromotion = new MultiProductPromotion(margeritaPizza, vegetarianPizza, 1.00);

            var orderedItems = new List<MenuItem>() { margeritaPizza, vegetarianPizza, vegetarianPizza1 };
            multiProductPromotion.ApplyPromotion(orderedItems);

            margeritaPizza.Price.Should().Be(1.50);
            margeritaPizza.DiscountedPrice.Should().Be(0.50); //discount given
            vegetarianPizza.Price.Should().Be(2.00);
            vegetarianPizza.DiscountedPrice.Should().Be(0.50); //discount given
            vegetarianPizza1.Price.Should().Be(2.00);
            vegetarianPizza1.DiscountedPrice.Should().Be(0); //discount Not given on extra vegetarian
        }

        [TestMethod]
        public void When_ApplyingPromotionThreeQualifyMenuItemsManyTimes_ShouldRecieveDiscountOnlyOnce()
        {
            var margeritaPizza = CreateMenuItem(1, "Margerita pizza", "cheese and tomato", 1.50);
            var vegetarianPizza = CreateMenuItem(2, "Vegetarian pizza", "cheese, tomato, onions, peppers", 2.00);
            var vegetarianPizza1 = CreateMenuItem(2, "Vegetarian pizza", "cheese, tomato, onions, peppers", 2.00);
            var polloPizza = CreateMenuItem(3, "Pollo pizza", "cheese tomato chicken", 2.50);

            MultiProductPromotion multiProductPromotion = new MultiProductPromotion(margeritaPizza, vegetarianPizza, 1.00);

            var orderedItems = new List<MenuItem>() { margeritaPizza, vegetarianPizza, vegetarianPizza1 };
            multiProductPromotion.ApplyPromotion(orderedItems);
            multiProductPromotion.ApplyPromotion(orderedItems);
            multiProductPromotion.ApplyPromotion(orderedItems);

            margeritaPizza.Price.Should().Be(1.50);
            margeritaPizza.DiscountedPrice.Should().Be(0.50); //discount given
            vegetarianPizza.Price.Should().Be(2.00);
            vegetarianPizza.DiscountedPrice.Should().Be(0.50); //discount given
            vegetarianPizza1.Price.Should().Be(2.00);
            vegetarianPizza1.DiscountedPrice.Should().Be(0); //discount Not given on extra vegetarian
        }


        private static MenuItem CreateMenuItem(int sku, string name, string description, double price)
        {
            return new MenuItem() { Sku = sku, Name = name, Description = description, Price = price };
        }
    }
}
