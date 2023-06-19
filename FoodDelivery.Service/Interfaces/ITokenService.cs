using FoodDelivery.Models.Entity;
using FoodDelivery.Models.ViewModel.User;

namespace FoodDelivery.Service.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
