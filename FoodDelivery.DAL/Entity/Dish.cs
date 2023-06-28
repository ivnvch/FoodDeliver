using FoodDelivery.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.DAL.Entity
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public short Amount { get; set; }
        public Category Category { get; set; }
        //[ForeignKey("OrderId")]
        //public int OrderId { get; set; }
        //[ForeignKey("VendorId")]
        //public int VendorId { get; set; }

    }
}