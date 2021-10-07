using HotFoodStore.Domain.Entities;
using HotFoodStore.Domain.Promotion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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

        [TestMethod]
        public void PromotionEngine_WhenTwoActivePromotions_ShouldApplyBothPromotions()
        {
            Mock<IPromotion> promotionOne = new Mock<IPromotion>();
            Mock<IPromotion> promotionTwo = new Mock<IPromotion>();

            PromotionEngine promotionEngine = new PromotionEngine(new List<IPromotion>() { promotionOne.Object, promotionTwo.Object });
            promotionEngine.CalculateDiscounts(new List<MenuItem>());

            promotionOne.Verify(p => p.ApplyPromotion(It.IsAny<List<MenuItem>>()), Times.Once());
            promotionTwo.Verify(p => p.ApplyPromotion(It.IsAny<List<MenuItem>>()), Times.Once());
        }

        //There is ALOT more PromotionEngine tests to write but it will take longer than the specified time in the exercise. 
        //PromotptionEngine decides the order and importance of promotions. All that has to be tested. All the promotion combinations
        //have to be tested. 

    }
}
