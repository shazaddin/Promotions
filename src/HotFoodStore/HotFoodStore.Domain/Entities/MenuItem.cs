namespace HotFoodStore.Domain.Entities
{
    public class MenuItem
    {
        public int Sku { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double DiscountedPrice { get; set; }
    }
}
