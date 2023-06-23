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
        [Authorize]
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var reviews = await _reviewService.GetListAsync();
                if (reviews == null)
                    return Ok("the review list is still empty");
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest("error when getting an review list:" + ex.Message);
            }
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
            try
            {
                var currentUser = HttpContext.User;
                int userId = _reviewService.GetUserByReviewIdAsync(reviewDto.Id).Id;
                if (userId == int.Parse(currentUser.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")) || currentUser.FindFirstValue(ClaimTypes.Role) == "Admin")
                    return await _reviewService.UpdateAsync(reviewDto) ? Ok("review has been updated") : BadRequest("review not updated");
                return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest("error when changing an review " + ex.Message);
            }
        }
        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var currentUser = HttpContext.User;
                var review = await _reviewService.GetByIdAsync(id);
                int userId = _reviewService.GetUserByReviewIdAsync(id).Id;
                if (userId == int.Parse(currentUser.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")) || currentUser.FindFirstValue(ClaimTypes.Role) == "Admin")
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
