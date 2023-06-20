using FoodDelivery.Models.Entity;
using FoodDelivery.Models.ViewModel;
using FoodDelivery.Models.ViewModel.User;
using FoodDelivery.Service.Implementations;

namespace FoodDelivery.Service.Interfaces
{
    public interface ITokenService
    {
       // string CreateToken(User user);
        AuthResponseModel GetToken(User user);
        Task<AuthResponseModel> Refresh(RefreshTokenModel model);
    }
}
