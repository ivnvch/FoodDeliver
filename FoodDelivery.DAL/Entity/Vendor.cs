using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.DAL.Entity
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<Dish> Dishes { get; set; } = new List<Dish> { };
        public List<double> AllCustomerRatings { get; set; } = new List<double> { };
    }
}
