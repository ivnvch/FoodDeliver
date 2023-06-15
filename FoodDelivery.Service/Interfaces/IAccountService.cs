using FoodDelivery.Models.Repsonse;
using FoodDelivery.Models.ViewModel.Account;
using System.Security.Claims;

namespace FoodDelivery.Service.Interfaces
{
    public interface IAccountService
    {
        Task<IBaseResponse<ClaimsIdentity>> Register(RegisterViewModel viewModel);
        Task<IBaseResponse<ClaimsIdentity>> Login(LoginViewModel ViewModel);
    }
}
