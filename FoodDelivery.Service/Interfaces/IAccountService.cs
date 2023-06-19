using FoodDelivery.Models.Entity;
using FoodDelivery.Models.ViewModel.Account;
using FoodDelivery.Models.ViewModel.User;


namespace FoodDelivery.Service.Interfaces
{
    public interface IAccountService
    {

        Task<UserViewModel> Register(RegisterViewModel registerViewModel);
        Task<UserViewModel> Login(LoginViewModel loginViewModel);
    }
}
