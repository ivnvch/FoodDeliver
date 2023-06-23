using FoodDelivery.Models.ViewModel.Vendor;
using FoodDelivery.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;
        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }
        //[Authorize]
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var vendors = await _vendorService.GetListAsync();
                if (vendors == null)
                    return Ok("the vendor list is still empty");
                return Ok(vendors);
            }
            catch (Exception ex)
            {
                return BadRequest("error when getting an vendor list:" + ex.Message);
            }
        }
        //[Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Post(VendorDto vendorDto)
        {
            return await _vendorService.CreateAsync(vendorDto) ? Ok("vendor has been created") : BadRequest("vendor not created");
        }
        //[Authorize]
        [HttpPut("Update")]
        public async Task<IActionResult> Put(VendorDto vendorDto)
        {
            try
            {
                var currentUser = HttpContext.User;
                var vendor = await _vendorService.GetByIdAsync(vendorDto.Id);

                // if (_vendorService.GetUserByBasketIdAsync(orderDto.BasketId).Id == int.Parse(currentUser.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")) || currentUser.FindFirstValue(ClaimTypes.Role) == "Admin")
                // {

                vendor.Type = vendorDto.Type;
                vendor.Name = vendorDto.Name;
                vendor.PhoneNumber = vendorDto.PhoneNumber;
                vendor.Address = vendorDto.Address;
                vendor.OpeningTime = vendorDto.OpeningTime;
                vendor.ClosingTime = vendorDto.ClosingTime;
                vendor.TimeOfDelivery = vendorDto.TimeOfDelivery;
                vendor.Description = vendorDto.Description;
                return await _vendorService.UpdateAsync(vendor) ? Ok("vendor has been updated") : BadRequest("vendor not updated");
                // }
                // return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest("error when changing an vendor " + ex.Message);
            }

        }
        //[Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var currentUser = HttpContext.User;
                var vendor = await _vendorService.GetByIdAsync(id);
                // if (vendor.Basket.UserId == int.Parse(currentUser.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")) || currentUser.FindFirstValue(ClaimTypes.Role) == "Admin")
                // {
                return await _vendorService.DeleteAsync(id) ? Ok("vendor has been removed") : BadRequest("vendor not deleted");
                // }
                // return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest("error when deleting an vendor: " + ex.Message);
            }
        }
    }
}

