namespace FoodDelivery.Models.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Basket Basket { get; set; }
        public Profile Profile { get; set; }
    }
}