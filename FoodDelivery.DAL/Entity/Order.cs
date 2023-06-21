using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.DAL.Entity
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public int DishId { get; set; }
        [ForeignKey("BasketId")]
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        public bool IsComplete { get; set; } = false;
    }
}