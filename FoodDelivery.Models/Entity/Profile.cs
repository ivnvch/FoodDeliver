namespace FoodDelivery.Models.Entity
{
    public class Profile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public int LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateCreated { get; set; }
        public string PhoneNumber { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}