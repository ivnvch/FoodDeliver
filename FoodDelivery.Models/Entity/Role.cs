using FoodDelivery.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.Entity
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public RoleType Name { get; set; }
        public List<User>? Users { get; set; }
    }
}
