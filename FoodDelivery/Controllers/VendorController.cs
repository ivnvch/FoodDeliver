using FluentValidation;
using FoodDelivery.DAL.Entity;
using FoodDelivery.Models.ViewModel.Review;
using FoodDelivery.Models.ViewModel.Vendor;
using FoodDelivery.Service.Interfaces;
using FoodDelivery.Service.Validators.AddValidator;
using FoodDelivery.Service.Validators.UpdateValidator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpGet("GetNumberReviews")]
        public async Task<IActionResult> GetNumberReviews(int id)
        {
            try
            {
                var numberReviews = await _vendorService.GetNumberReviewsAsync(id);
                return Ok(numberReviews);
            }
            catch (Exception ex)
            {
                return BadRequest("error getting number of reviews " + ex.Message);
            }
        }
        [HttpGet("GetСustomerRating")]
        public async Task<IActionResult> GetСustomerRating(int id)
        {
            try
            {
                var vendors = await _vendorService.GetСustomerRatingAsync(id);
                return Ok(vendors);
            }
            catch (Exception ex)
            {
                return BadRequest("error when getting customer rating " + ex.Message);
            }
        }
        [HttpGet("SortingByDeliveryTime")]
        public async Task<IActionResult> SortingByDeliveryTime()
        {
            try
            {
                var vendors = await _vendorService.SortingByDeliveryTimeAsync();
                return Ok(vendors);
            }
            catch (Exception ex)
            {
                return BadRequest("error getting number of reviews " + ex.Message);
            }
        }
        [HttpGet("SortingByRating")]
        public async Task<IActionResult> SortingByRating()
        {
            try
            {
                var vendors = await _vendorService.SortingByRatingAsync();
                return Ok(vendors);
            }
            catch (Exception ex)
            {
                return BadRequest("error when getting customer rating " + ex.Message);
            }
        }
        [Authorize(Roles = "Vendor")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post(VendorDto vendorDto)
        {
            var validator = new AddVendorValidator();
            var validationResult = validator.Validate(vendorDto);
            if (validationResult.IsValid)
            {
                return await _vendorService.CreateAsync(vendorDto) ? Ok("vendor has been created") : BadRequest("vendor not created");
            }
            else
            {
                return BadRequest("entry is not correct");
            }
        }
        [Authorize(Roles = "Vendor")]
        [HttpPut("Update")]
        public async Task<IActionResult> Put(VendorDto vendorDto)
        {
            var validator = new UpdateVendorValidator();
            var validationResult = validator.Validate(vendorDto);
            if (validationResult.IsValid)
            {
                return await _vendorService.UpdateAsync(vendorDto) ? Ok("vendor has been updated") : BadRequest("vendor not updated");
            }
            else
            {
                return BadRequest("entry is not correct");
            }
        }
        [Authorize(Roles = "Vendor")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _vendorService.DeleteAsync(id) ? Ok("vendor has been removed") : BadRequest("vendor not deleted");
            }
            catch (Exception ex)
            {
                return BadRequest("error when deleting an vendor: " + ex.Message);
            }
        }
    }
}

