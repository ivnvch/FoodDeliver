using FoodDelivery.Models.Helpers;
using FoodDelivery.Models.ViewModel.Profile;
using FoodDelivery.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {

            string login = IdentityHelper.GetLogin(User);
            var profile = await _profileService.GetProfile(login);

            if (profile.StatusCode == Models.Enum.StatusCode.OK)
            {
                return Ok(profile);
            }

            return NoContent();
        }

        [HttpPut("SaveProfile")]
        public async Task<IActionResult> Save(ProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _profileService.Save(viewModel);

                if (response.StatusCode == Models.Enum.StatusCode.OK)
                {
                    return Ok();
                }
            }

            return NoContent();
        }
    }
}
