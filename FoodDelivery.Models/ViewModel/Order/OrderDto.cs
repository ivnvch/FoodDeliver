namespace FoodDelivery.Models.ViewModel.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public int DishId { get; set; }
        public int BasketId { get; set; }
        public bool IsComplete { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? Commentary { get; set; } = string.Empty;
    }
}
