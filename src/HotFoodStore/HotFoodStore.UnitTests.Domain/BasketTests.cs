using FluentAssertions;
using HotFoodStore.Domain.Entities;
using HotFoodStore.Domain.Promotion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace HotFoodStore.UnitTests.Domain
{
    [TestClass]
    public class BasketTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Value cannot be null")]
        public void When_PromotionEngineNull_ShouldThrowException()
        {
            Basket basket = new Basket(null); 
        }

        [TestMethod]
        public void When_PromotionEngineHasNoActivePromotions_ShouldFunctionNormally()
        {
            Basket basket = new Basket(new PromotionEngine(new List<IPromotion>()));
        }

        [TestMethod]
        public void When_AddMenuItemToBasket_ProductCountShouldIncreaseByOne()
        {
            Basket basket = new Basket(new PromotionEngine(new List<IPromotion>()));
            basket.Add(CreateMenuItem(1, "Margerita", "pizza", 2.00));
            basket.Products.Count.Should().Be(1); 
        }

        [TestMethod]
        public void When_ClearBasket_ProductCountShouldBeZero()
        {
            Basket basket = new Basket(new PromotionEngine(new List<IPromotion>()));
            basket.Add(CreateMenuItem(1, "Margerita", "pizza", 2.00));
            basket.Add(CreateMenuItem(1, "Margerita", "pizza", 2.00));
            basket.Products.Count.Should().Be(2);
            basket.Clear();
            basket.Products.Count.Should().Be(0);
        }

        private static MenuItem CreateMenuItem(int sku, string name, string description, double price)
        {
            return new MenuItem() { Sku = sku, Name = name, Description = description, Price = price };
        }
    }
}
