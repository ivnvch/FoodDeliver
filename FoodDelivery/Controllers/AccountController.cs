using FoodDelivery.DAL.Interfaces;
using FoodDelivery.Models.Helpers;
using FoodDelivery.Models.ViewModel.Account;
using FoodDelivery.Models.ViewModel.User;
using FoodDelivery.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var userRegister = await _accountService.Register(registerViewModel);
                return Ok(userRegister);
            }

            return Unauthorized("Не все поля формы были заполнены.");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var loginUser = await _accountService.Login(loginViewModel);
                return Ok(loginUser);
            }

            return Unauthorized("Проверьте правильность введённых данных.");
        }
    }
}
