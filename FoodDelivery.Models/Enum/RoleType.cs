using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDelivery.Models.Enum
{
    public enum RoleType
    {
        [Display(Name = "User")]
        User = 1,
        [Display(Name = "Admin")]
        Admin = 2,
    }
}
