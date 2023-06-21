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


        public async Task<IBaseResponse<User>> CreateUser(UserViewModel viewModel)
        {
            try
            {
                User user = (User) await _unitOfWork.UserRepository.FindByConditionAsync(x => x.Login == viewModel.Login);

                if (user != null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = "Пользователь с таким логином уже существует!",
                        StatusCode = Models.Enum.StatusCode.UserAlreadyExist,
                    };
                }

                user = new User()
                {
                    Login = viewModel.Login,
                    //Password = viewModel.Password,//нужно захешировать пароль
                };

                await _unitOfWork.UserRepository.CreateAsync(user);
                await _unitOfWork.SaveAsync();

                var profile = new Profile()
                {
                    LastName = string.Empty,
                    FirstName = string.Empty,
                    MiddleName = string.Empty,
                    PhoneNumber = string.Empty,
                    DateCreated = DateTime.Now,
                    UserId = user.Id,
                };

                await _unitOfWork.ProfileRepository.CreateAsync(profile);
                await _unitOfWork.SaveAsync();

                return new BaseResponse<User>()
                {
                    Data = user,
                    Description = "Аккаунт создан",
                    StatusCode = StatusCode.OK,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутреняя ошибка {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteUser(int id)
        {
            try
            {
                User user = (User) await _unitOfWork.UserRepository.FindByConditionAsync(x => x.Id == id);

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
            var user = (User) await _unitOfWork.UserRepository.FindByConditionAsync( x => x.Login == userToken);

            if (user is null)
            {
                throw new Exception("User not Found");
            }

            return user;
        }


        //public async Task<IBaseResponse<IEnumerable<UserViewModel>>> GetUsers()
        //{
        //    try
        //    {
        //        var users = await _unitOfWork.UserRepository.GetAllAsync().
        //            Select(x => new UserViewModel()
        //            {
        //                Id = x.Id,
        //                Login = x.Login,

        //            }).ToListAsync();

        //        return new BaseResponse<IEnumerable<UserViewModel>>()
        //        {
        //            Data = users,
        //            StatusCode = StatusCode.OK,
        //        };

        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<IEnumerable<UserViewModel>>()
        //        {
        //            Description = $"Внутренняя ошибка {ex.Message}",
        //            StatusCode = StatusCode.InternalServerError,
        //        };
        //    }
        //}
    }
}
