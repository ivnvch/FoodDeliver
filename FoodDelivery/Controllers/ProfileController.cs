using FoodDelivery.Models.Helpers;
using FoodDelivery.Models.ViewModel.Profile;
using FoodDelivery.Service.Interfaces;
using FoodDelivery.Service.Validators.UpdateValidator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            //string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            var profile = await _profileService.GetProfile(login);

            if (profile.StatusCode == Models.Enum.StatusCode.OK && profile != null)
            {
                return Ok(profile);
            }

            return NoContent();
        }

        [HttpPut("SaveProfile")]
        public async Task<IActionResult> Save(ProfileViewModel viewModel)
        {
            UpdateProfileValidator validator = new UpdateProfileValidator();
            var validatorResult = validator.Validate(viewModel);
            if (validatorResult.IsValid)
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
