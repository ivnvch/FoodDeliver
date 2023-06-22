namespace FoodDelivery.Models.ViewModel.DTOs
{
    public class VendorDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }
        public DateTime TimeOfDelivery { get; set; }
        public string? Description { get; set; } = string.Empty;
    }
}
