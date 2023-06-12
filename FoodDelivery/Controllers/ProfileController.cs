using FoodDelivery.Models.Entity;
using FoodDelivery.Models.ViewModel.Profile;
using FoodDelivery.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var login = User.Identity.Name;
            var profile = await _profileService.GetProfile(login);

            if (profile.StatusCode == Models.Enum.StatusCode.OK)
            {
                return Ok(profile);
            }

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Save(ProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _profileService.Save(viewModel);

                if (response.StatusCode == Models.Enum.StatusCode.OK)
                {
                    return NoContent();
                }
            }

            return BadRequest();
        }
    }
}
