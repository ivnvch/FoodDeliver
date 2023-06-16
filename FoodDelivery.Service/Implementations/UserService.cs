
using FoodDelivery.DAL.Interfaces;
using FoodDelivery.Models.Entity;
using FoodDelivery.Models.Enum;
using FoodDelivery.Models.Repsonse;
using FoodDelivery.Models.ViewModel.User;
using FoodDelivery.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Profile> _profileRepository;

        public UserService(IBaseRepository<User> userRepository, IBaseRepository<Profile> profileRepository)
        {
            _userRepository = userRepository;
            _profileRepository = profileRepository;
        }

        public async Task<IBaseResponse<User>> CreateUser(UserViewModel viewModel)
        {
            try
            {
                var user = await _userRepository.GetAllAsync().FirstOrDefaultAsync(x => x.Login == viewModel.Login);

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

                await _userRepository.CreateAsync(user);

                var profile = new Profile()
                {
                    LastName = string.Empty,
                    FirstName = string.Empty,
                    MiddleName = string.Empty,
                    PhoneNumber = string.Empty,
                    DateCreated = DateTime.Now,
                    UserId = user.Id,
                };

                await _profileRepository.CreateAsync(profile);

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
                var user = await _userRepository.GetAllAsync().FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Аккаунт не найден",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false,
                    };
                }

                await _userRepository.DeleteAsync(user);

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

        public async Task<IBaseResponse<IEnumerable<UserViewModel>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAllAsync().
                    Select(x => new UserViewModel()
                    {
                        Id = x.Id,
                        Login = x.Login,

                    }).ToListAsync();

                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data = users,
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
