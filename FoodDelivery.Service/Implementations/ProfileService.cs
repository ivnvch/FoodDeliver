using FoodDelivery.DAL.Interfaces;
using FoodDelivery.Models.Entity;
using FoodDelivery.Models.Enum;
using FoodDelivery.Models.Repsonse;
using FoodDelivery.Models.ViewModel.Profile;
using FoodDelivery.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Service.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IBaseRepository<Profile> _profileRepository;

        public ProfileService(IBaseRepository<Profile> profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<IBaseResponse<ProfileViewModel>> GetProfile(string login)
        {
            try
            {
                var profile = await _profileRepository.GetAllAsync()
                    .Select(x => new ProfileViewModel() 
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        MiddleName = x.MiddleName,
                        DateCreated = x.DateCreated,
                        PhoneNumber = x.PhoneNumber,
                        Login = x.User.Login,
                    })
                    .FirstOrDefaultAsync(x => x.Login == login);

                return new BaseResponse<ProfileViewModel>()
                {
                    Data = profile,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProfileViewModel>()
                {
                    Description = $"Внутренняя ошибка: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Profile>> Save(ProfileViewModel viewModel)
        {
            try
            {
                var profile = await _profileRepository.GetAllAsync().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

                profile.FirstName = viewModel.FirstName;
                profile.LastName = viewModel.LastName;
                profile.MiddleName = viewModel.MiddleName;
                profile.DateCreated = viewModel.DateCreated;
                profile.PhoneNumber = viewModel.PhoneNumber;

                _profileRepository.UpdateAsync(profile);

                return new BaseResponse<Profile>()
                {
                    Data = profile,
                    StatusCode = StatusCode.OK,
                    Description = "Данные обновлены",
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<Profile>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}",
                };
            }
        }
    }
}
