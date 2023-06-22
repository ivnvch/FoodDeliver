using FoodDelivery.Models.ViewModel;
using FoodDelivery.Models.ViewModel.Account;
using FoodDelivery.Models.ViewModel.User;
using FoodDelivery.Service.Implementations;


namespace FoodDelivery.Service.Interfaces
{
    public interface IAccountService
    {
        Task<AuthResponseModel> Register(RegisterViewModel registerViewModel);
        Task<AuthResponseModel> Login(LoginViewModel loginViewModel);

    }
}
