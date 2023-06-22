using FoodDelivery.Models.ViewModel.Account;
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
            try
            {
                if (ModelState.IsValid)
                {
                    var userRegister = await _accountService.Register(registerViewModel);
                    return Ok(userRegister);
                }

                return BadRequest("Invalid Data");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var loginUser = await _accountService.Login(loginViewModel);
                return Ok(loginUser);
            }

            return BadRequest("Invalid Data");
        }
    }
}
