using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.ViewModel.Account
{
    public class LoginViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
