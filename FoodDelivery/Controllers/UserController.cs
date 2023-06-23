using FoodDelivery.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _userService.GetUsers();
            if (response.StatusCode == Models.Enum.StatusCode.OK)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
            if (response.StatusCode == Models.Enum.StatusCode.OK)
            {
                return Ok();
            }

            return BadRequest();
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateOwner(UserViewModel userViewModel)
        //{
        //    var response = await _userService.CreateUser(userViewModel);

        //    if (response.StatusCode == Models.Enum.StatusCode.OK)
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();
        //}

    }
}
