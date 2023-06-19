using FoodDelivery.DAL.Interfaces;
using FoodDelivery.Models.Entity;
using FoodDelivery.Models.Helpers;
using FoodDelivery.Models.ViewModel.Account;
using FoodDelivery.Models.ViewModel.User;
using FoodDelivery.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly ITokenService _tokenService;

        public AccountService(IBaseRepository<User> userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<UserViewModel> Login(LoginViewModel loginViewModel)
        {
            var user = await _userRepository.GetAllAsync().FirstOrDefaultAsync(x => x.Login == loginViewModel.Login);

            if (user == null)
            {
                throw new Exception("Incorrect login");
            }

            if (!PasswordHashHelpers.VerifyPassword(loginViewModel.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Incorrect password");
            }

            var token = _tokenService.CreateToken(user);

            return new UserViewModel()
            {
                Login = loginViewModel.Login,
                Token = token,

            };
        }

        public async Task<UserViewModel> Register(RegisterViewModel registerViewModel)
        {
            var user = await _userRepository.GetAllAsync().FirstOrDefaultAsync(x => x.Login == registerViewModel.Login);

            if (user != null)
            {
                throw new Exception("This login already exists");
            }

            byte[] passwordHash, passwordSalt;//?
            PasswordHashHelpers.CreatePasswordHash(registerViewModel.Password, out passwordHash, out passwordSalt);
             user = new User
             {
                Login = registerViewModel.Login,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
             };

            await _userRepository.CreateAsync(user);

            return new UserViewModel
            {
                Login = user.Login,
                Token = _tokenService.CreateToken(user),
            };
        }
    }
}
