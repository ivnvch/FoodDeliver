using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.ViewModel.User
{
    public class UserViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        //[Required(ErrorMessage = "Укажите роль")]
        //[Display(Name = "Роль")]
        //public string Role { get; set; }

        [Required(ErrorMessage = "Укажите логин")]
        [Display(Name = "Логин")]
        public string Login { get; set; }
        public string Token { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
