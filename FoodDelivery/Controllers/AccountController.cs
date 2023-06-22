using FoodDelivery.DAL.Interfaces;
using FoodDelivery.Models.Helpers;
using FoodDelivery.Models.ViewModel.Account;
using FoodDelivery.Models.ViewModel.User;
using FoodDelivery.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            try
            {
                var userRegister = await _accountService.Register(registerViewModel);
                return Ok(userRegister);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (loginViewModel == null)
            {
                return BadRequest("Invalid Data");
            }

            var loginUser = await _accountService.Login(loginViewModel);
            return Ok(loginUser);
        }
    }
}
