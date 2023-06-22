namespace FoodDelivery.Models.ViewModel.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public int DishId { get; set; }
        public int BasketId { get; set; }
        public bool IsComplete { get; set; }
    }
}
