using FoodDelivery.DAL.Entity;
using FoodDelivery.DAL.Interfaces;
using FoodDelivery.Models.Helpers;
using FoodDelivery.Models.ViewModel;
using FoodDelivery.Models.ViewModel.Account;
using FoodDelivery.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public AccountService(ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResponseModel> Login(LoginViewModel loginViewModel)
        {
            var response = await _unitOfWork.UserRepository.FindByConditionAsync(x => x.Login == loginViewModel.Login);
            User user = await response.FirstOrDefaultAsync();

            if (user is null)
            {
                throw new Exception("Incorrect login");
            }

            if (!PasswordHasher.VerifyPassword(loginViewModel.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Incorrect password");
            }

            var token = _tokenService.GetToken(user);

            return token;
        }

        public async Task<AuthResponseModel> Register(RegisterViewModel registerViewModel)
        {
            User? user = await _unitOfWork.UserRepository.FindByConditionAsync(x => x.Login == registerViewModel.Login) as User;

            if (user != null)
            {
                throw new Exception("This login already exists");
            }

            byte[] passwordHash, passwordSalt;//?
            PasswordHasher.PasswordHash(registerViewModel.Password, out passwordHash, out passwordSalt);
             user = new User
             {
                Login = registerViewModel.Login,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = Models.Enum.Role.User,
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

            Basket basket = new Basket()
            {
                UserId = user.Id,
            };
            await _unitOfWork.BasketRepository.CreateAsync(basket);

            await _unitOfWork.SaveAsync();


            return _tokenService.GetToken(user);
        }

    }
}
