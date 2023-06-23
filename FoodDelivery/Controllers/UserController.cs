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
            if (User.IsInRole("Admin"))
            {
                var response = await _userService.GetUsers();
                if (response.StatusCode == Models.Enum.StatusCode.OK)
                {
                    return Ok(response);
                }
            }
            return Unauthorized("Отказано в доступе");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
            if (response.StatusCode == Models.Enum.StatusCode.OK)
            {
                return Ok();
            }

            return Unauthorized("Отказано в доступе");
        }

    }
}
