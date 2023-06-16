using FoodDelivery.Models.Enum;

namespace FoodDelivery.Models.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        //public string Password { get; set; }
        //public Role Role { get; set; }
        public Basket Basket { get; set; }
        public Profile Profile { get; set; }
    }
}