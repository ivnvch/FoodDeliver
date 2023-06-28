using FoodDelivery.Models.ViewModel.Review;
using FoodDelivery.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _reviewService.GetListAsync());
        }
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Post(ReviewDto reviewDto)
        {
            return await _reviewService.CreateAsync(reviewDto) ? Ok("review has been created") : BadRequest("review not created");
        }
        [Authorize]
        [HttpPut("Update")]
        public async Task<IActionResult> Put(ReviewDto reviewDto)
        {
            var currentUser = HttpContext.User;
            if (currentUser.FindFirstValue(ClaimTypes.Role) == "Admin" || currentUser.FindFirstValue(ClaimTypes.Role) == "User")
                return await _reviewService.UpdateAsync(reviewDto) ? Ok("review has been updated") : BadRequest("review not updated");
            return BadRequest("you do not have access to perform this action");
        }
        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var currentUser = HttpContext.User;
                if (currentUser.FindFirstValue(ClaimTypes.Role) == "Admin" || currentUser.FindFirstValue(ClaimTypes.Role) == "User")
                    return await _reviewService.DeleteAsync(id) ? Ok("review has been removed") : BadRequest("review not deleted");
                return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest("error when deleting an review: " + ex.Message);
            }
        }
    }
}
