using FoodDelivery.DAL.Entity;
using FoodDelivery.DAL.Interfaces;
using FoodDelivery.Models.Enum;
using FoodDelivery.Models.Repsonse;
using FoodDelivery.Models.ViewModel.Profile;
using FoodDelivery.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Service.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IBaseResponse<ProfileViewModel>> GetProfile(string login)
        {
            try
            {
                var serchProfile = await _unitOfWork.ProfileRepository.FindByConditionAsync(x => x.User.Login == login);

                var profileModel =  await serchProfile.Select(x => new ProfileViewModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    MiddleName = x.MiddleName,
                    DateCreated = x.DateCreated,
                    PhoneNumber = x.PhoneNumber,
                    Login = x.User.Login,
                }).FirstOrDefaultAsync();

                return new BaseResponse<ProfileViewModel>()
                {
                    Data = profileModel,
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

                var profile = await _unitOfWork.ProfileRepository.FindByConditionAsync(x => x.Id == viewModel.Id).Result.FirstOrDefaultAsync();

                profile.FirstName = viewModel.FirstName;
                profile.LastName = viewModel.LastName;
                profile.MiddleName = viewModel.MiddleName;
                profile.DateCreated = viewModel.DateCreated;
                profile.PhoneNumber = viewModel.PhoneNumber;


                await _unitOfWork.ProfileRepository.UpdateAsync(profile);

                await _unitOfWork.SaveAsync();

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
