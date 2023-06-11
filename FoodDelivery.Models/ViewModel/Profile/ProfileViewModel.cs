
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.ViewModel.Profile
{
    public class ProfileViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите Имя")]
        [MinLength(2, ErrorMessage = "Минимальная длина имени составляет 2 символа")]
        [MaxLength(25, ErrorMessage = "Максимальная длина имени может составлять 25 символов")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Укажите Фамилию")]
        [MinLength(2, ErrorMessage = "Минимальная длина фамилии составляет 2 символа")]
        [MaxLength(50, ErrorMessage = "Максимальная длина фамилии может составлять 50 символов")]
        public string LastName { get; set; }

        [MinLength(5, ErrorMessage = "Минимальная длина отчества составляет 5 символа")]
        [MaxLength(50, ErrorMessage = "Максимальная длина отчества может составлять 50 символов")]
        public string MiddleName { get; set; }
        public DateTime DateCreated { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
    }
}
