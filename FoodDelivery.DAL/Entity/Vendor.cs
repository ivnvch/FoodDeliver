using FoodDelivery.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DAL.Entity
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }
        public DateTime TimeOfDelivery { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double? CustomerRaiting { get; set; }
        public List<Dish> Dishes { get; set; } = new List<Dish> { };
        public List<Review> Reviews { get; set; } = new List<Review> { };
        public string? Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

    }
}
