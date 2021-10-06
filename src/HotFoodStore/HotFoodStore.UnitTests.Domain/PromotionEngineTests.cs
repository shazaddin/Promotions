using HotFoodStore.Domain.Entities;
using HotFoodStore.Domain.Promotion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace HotFoodStore.UnitTests.Domain
{
    [TestClass]
    public class PromotionEngineTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Value cannot be null")]
        public void PromotionEngine_WhenNullPromotionsArePassedIn_ShouldThrowException()
        {
            PromotionEngine promotionEngine = new PromotionEngine(null);
        }

        [TestMethod]
        public void PromotionEngine_WhenZeroActivePromotions_ShouldInitialise()
        {
            PromotionEngine promotionEngine = new PromotionEngine(new List<IPromotion>());
            promotionEngine.CalculateDiscounts(new List<MenuItem>()); 
        }
    }
}
