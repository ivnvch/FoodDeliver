using FoodDelivery.DAL.Entity;
using FoodDelivery.DAL.Interfaces;
using FoodDelivery.Models.Helpers;
using FoodDelivery.Models.ViewModel;
using FoodDelivery.Models.ViewModel.Account;
using FoodDelivery.Models.ViewModel.User;
using FoodDelivery.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace FoodDelivery.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Profile> _profileRepository;
        private readonly ITokenService _tokenService;

        public AccountService(IBaseRepository<User> userRepository, ITokenService tokenService, IBaseRepository<Profile> profileRepository)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _profileRepository = profileRepository;
        }

        [Authorize(Roles = "User")]
        public async Task<AuthResponseModel> Login(LoginViewModel loginViewModel)
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

            var token = _tokenService.GetToken(user);

            return token;
        }

        public async Task<AuthResponseModel> Register(RegisterViewModel registerViewModel)
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
                PasswordSalt = passwordSalt,
                Role = Models.Enum.Role.User,
             };

            await _userRepository.CreateAsync(user);

            var profile = new Profile()
            {
                UserId = user.Id,
                FirstName = "Diamond",
                DateCreated = DateTime.Now,
                LastName = "daa",
                MiddleName = "d ",
                PhoneNumber = "asd",
            };

            await _profileRepository.CreateAsync(profile);


            return _tokenService.GetToken(user);
        }

    }
}
