using FoodDelivery.DAL.Entity;
using FoodDelivery.Models.Repsonse;
using FoodDelivery.Models.ViewModel.Profile;

namespace FoodDelivery.Service.Interfaces
{
    public interface IProfileService
    {
        Task<IBaseResponse<Profile>> Save(ProfileViewModel viewModel);
        Task<IBaseResponse<ProfileViewModel>> GetProfile(string login);
    }
}
