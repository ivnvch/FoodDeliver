using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.Enum
{
    public enum Category
    {
        [Display(Name = "Суши")]
        Sushi = 1,
        [Display(Name = "Пицца")]
        Pizza = 2,
        [Display(Name = "Бургеры")]
        Burger = 3,
        [Display(Name = "Шаурма")]
        Shawarma = 4,
        [Display(Name = "Шашлыки")]
        Kebab = 5,
        [Display(Name = "Бизнес-ланч")]
        BusinessLunch = 6,
        [Display(Name = "Торты")]
        Cake = 7,

    }
}
