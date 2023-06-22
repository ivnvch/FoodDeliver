using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public List<Vendor> Dishes { get; set; } = new List<Vendor> { };
        public List<Review> Comments { get; set; } = new List<Review> { };
        public string? Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
