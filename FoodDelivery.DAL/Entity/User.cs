using FoodDelivery.Models.Enum;

namespace FoodDelivery.DAL.Entity
{
    public class User
    {
        public int Id { get; set; }//Guid
        public string Login { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public Role Role { get; set; }
        public Basket Basket { get; set; }
        public Profile Profile { get; set; }
    }
}