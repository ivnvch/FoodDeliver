
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.ViewModel.Profile
{
    public class ProfileViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Укажите Имя")]
        [MinLength(2, ErrorMessage = "Минимальная длина имени составляет 2 символа")]
        [MaxLength(25, ErrorMessage = "Максимальная длина имени может составлять 25 символов")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Укажите Фамилию")]
        [MinLength(2, ErrorMessage = "Минимальная длина фамилии составляет 2 символа")]
        [MaxLength(50, ErrorMessage = "Максимальная длина фамилии может составлять 50 символов")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        [MinLength(5, ErrorMessage = "Минимальная длина отчества составляет 5 символа")]
        [MaxLength(50, ErrorMessage = "Максимальная длина отчества может составлять 50 символов")]
        public string MiddleName { get; set; }

        [Display(Name = "Дата регистрации аккаунта")]
        [DisplayFormat(DataFormatString = "{f}", ApplyFormatInEditMode = false)] /*dd.MM.yyyy*/
        public DateTime DateCreated { get; set; }

        [Display(Name = "Номер мобильного телефона")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\+\d{3}\(\d{2}\)\d{3}-\d{2}-\d{2}", ErrorMessage = "Введите номер телефона в формате +375(XX)XXX-XX-XX")]//сделать
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
    }
}
