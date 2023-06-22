using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.DAL.Entity
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User? User { get; set; }
        [ForeignKey("VendorId")]
        public int VendorId { get; set; }
        public Vendor? Vendor { get; set; }
        public double CustomerRating { get; set; } 
        public string? Description { get; set; } = string.Empty;
    }
}
