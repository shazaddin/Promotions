using HotFoodStore.Domain.Entities;
using System.Collections.Generic;

namespace HotFoodStore.Domain.Promotion
{
    public interface IPromotion
    {
        string Name { get; set; }

        void ApplyPromotion(List<MenuItem> allOrderedItems);
    }
}