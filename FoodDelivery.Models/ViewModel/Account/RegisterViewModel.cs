using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.ViewModel.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Для регистрации аккаунта необходимо ввести логин")]
        [MaxLength(100, ErrorMessage = "Максимальная длина логина должна составлять 100 символов")]
        [MinLength(3, ErrorMessage = "Минимльная длина логина должна быть не менее 3 символов")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(6, ErrorMessage = "Минимльна длина пароля должна составлять 6 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
