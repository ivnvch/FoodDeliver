using FoodDelivery.DAL.Interfaces;
using FoodDelivery.Models.Entity;
using FoodDelivery.Models.ViewModel.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IBaseRepository<User> _userRepository;

        public AccountController(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterViewModel registerViewModel)
        {
            var hmac = new HMACSHA512();
            var user = new User
            {
                Login = registerViewModel.Login,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerViewModel.Password)),
                PasswordSalt = hmac.Key,
            };

            await _userRepository.CreateAsync(user);

            return user;
        }
    }
}
