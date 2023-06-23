using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.ViewModel.Profile
{
    public class ProfileViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        //[Display(Name = "Дата регистрации аккаунта")]
        //[DisplayFormat(DataFormatString = "{f}", ApplyFormatInEditMode = false)] /*dd.MM.yyyy*/
        public DateTime DateCreated { get; set; }

        //[Display(Name = "Номер мобильного телефона")]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"\+\d{3}\(\d{2}\)\d{3}-\d{2}-\d{2}", ErrorMessage = "Введите номер телефона в формате +375(XX)XXX-XX-XX")]//сделать
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
    }
}
