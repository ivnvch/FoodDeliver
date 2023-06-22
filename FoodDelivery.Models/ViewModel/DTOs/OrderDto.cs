using FoodDelivery.Models.ViewModel.Dish;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Models.ViewModel.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public int DishId { get; set; }
        public int BasketId { get; set; }
        public bool IsComplete { get; set; }
        public decimal Price { get; set; }
        //public List<DishViewModel> Dishes { get; set; } = new List<DishViewModel> { };
        public int UserId { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? Commentary { get; set; } = string.Empty;
    }
}
