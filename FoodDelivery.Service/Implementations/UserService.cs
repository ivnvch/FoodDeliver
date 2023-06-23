using FoodDelivery.DAL.Entity;
using FoodDelivery.DAL.Interfaces;
using FoodDelivery.Models.Enum;
using FoodDelivery.Models.Repsonse;
using FoodDelivery.Models.ViewModel.User;
using FoodDelivery.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IBaseResponse<bool>> DeleteUser(int id)
        {
            try
            {
                User user = await _unitOfWork.UserRepository.FindByConditionAsync(x => x.Id == id)?.Result.FirstOrDefaultAsync();

                if (user is null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Аккаунт не найден",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false,
                    };
                }

                await _unitOfWork.UserRepository.DeleteAsync(user);
                await _unitOfWork.SaveAsync();

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                    Description = "Аккаунт удалён",
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"Внутреняя ошибка {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<User> GetUser(string userToken)
        {
            User user =  await _unitOfWork.UserRepository.FindByConditionAsync( x => x.Login == userToken).Result.FirstOrDefaultAsync();

            if (user is null)
            {
                throw new Exception("User not Found");
            }

            return user;
        }


        public async Task<IBaseResponse<IEnumerable<UserViewModel>>> GetUsers()
        {
            try
            {
                 var users = await _unitOfWork.UserRepository.GetAllAsync();

                List<UserViewModel> usersModel = new List<UserViewModel>();

                usersModel = await users.Select(x => new UserViewModel()
                {
                    Id = x.Id,
                    Login = x.Login,
                    Role = x.Role.ToString(),//переделать
                    Password = x.PasswordHash.ToString()
                }).ToListAsync();
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data = usersModel,
                    StatusCode = StatusCode.OK,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Description = $"Внутренняя ошибка {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }
    }
}
