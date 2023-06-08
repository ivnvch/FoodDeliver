namespace FoodDelivery.Models.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public int? DishId { get; set; }
        public int BasketId { get; set; }
        public Basket Basket { get; set; }

    }
}