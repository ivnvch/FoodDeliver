namespace FoodDelivery.Models.ViewModel.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public double CustomerRating { get; set; }
        public string? Description { get; set; } = string.Empty;
    }
}
