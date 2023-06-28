using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.DAL.Entity
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("BasketId")]
        public int BasketId { get; set; }
        public Basket? Basket { get; set; }
        public List<Dish> Dishes { get; set; } = new List<Dish> {};
        public bool IsComplete { get; set; } = false;
        public string Address { get; set; } = string.Empty;
        public string? Commentary { get; set; } = string.Empty; 

    }
}