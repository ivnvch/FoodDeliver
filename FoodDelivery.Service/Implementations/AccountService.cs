using FoodDelivery.DAL.Interfaces;
using FoodDelivery.Models.Entity;
using FoodDelivery.Models.Enum;
using FoodDelivery.Models.Repsonse;
using FoodDelivery.Models.ViewModel.Account;
using FoodDelivery.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace FoodDelivery.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Profile> _profileRepository;
        private readonly IBaseRepository<Basket> _basketRepository;

        public AccountService(IBaseRepository<User> userRepository, IBaseRepository<Profile> profileRepository, IBaseRepository<Basket> basketRepository)
        {
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _basketRepository = basketRepository;
        }

        public async Task<IBaseResponse<ClaimsIdentity>> Register(RegisterViewModel viewModel)
        {
            try
            {
                var user = await _userRepository.GetAllAsync().FirstOrDefaultAsync(x => x.Login == viewModel.Login);

                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь с таким логином уже существует. Выберите другой логин.",
                        StatusCode = StatusCode.UserAlreadyExist
                    };
                }

                user = new User()
                {
                    Login = viewModel.Login,
                    Password = viewModel.Password,
                };

                await _userRepository.CreateAsync(user);

                var profile = new Profile()
                {
                    UserId = user.Id,
                };

                var basket = new Basket()
                {
                    UserId = user.Id,
                };

                await _profileRepository.CreateAsync(profile);
                await _basketRepository.CreateAsync(basket);

                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK,
                    Description = "Аккаунт Успешно зарегистрирован"
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }


        public async Task<IBaseResponse<ClaimsIdentity>> Login(LoginViewModel viewModel)
        {
            try
            {
                var user = await _userRepository.GetAllAsync().FirstOrDefaultAsync(x => x.Login == viewModel.Login);

                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Проверьте правильность введённых данных",
                    };
                }

                if (user.Password != viewModel.Password)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Проверьте правильность введённых данных",
                    };
                }

                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = ex.Message,
                };
            }
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
            };

            return new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
