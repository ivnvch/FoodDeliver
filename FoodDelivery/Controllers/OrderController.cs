using FluentValidation;
using FoodDelivery.Models.ViewModel.Order;
using FoodDelivery.Models.ViewModel.Vendor;
using FoodDelivery.Service.Interfaces;
using FoodDelivery.Service.Validators.AddValidator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        //admin
        [Authorize]
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var orders = await _orderService.GetListAsync();
                if (orders == null)
                    return Ok("the order list is still empty");
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest("error when getting an order list:" + ex.Message);
            }
        }
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Post(OrderDto orderDto)
        {
            var validator = new UpdateOrderValidator();
            var validationResult = validator.Validate(orderDto);
            if (validationResult.IsValid)
            {
                return await _orderService.CreateAsync(orderDto) ? Ok("order has been created") : BadRequest("order not created");
            }
            else
            {
                return BadRequest("entry is not correct");
            }
        }
        [Authorize]
        [HttpPut("Update")]
        public async Task<IActionResult> Put(OrderDto orderDto)
        {
            var validator = new UpdateOrderValidator();
            var validationResult = validator.Validate(orderDto);
            if (validationResult.IsValid)
            {
                var currentUser = HttpContext.User;
                if (_orderService.GetUserByBasketIdAsync(orderDto.BasketId).Id == int.Parse(currentUser.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")) || currentUser.FindFirstValue(ClaimTypes.Role) == "Admin" || currentUser.FindFirstValue(ClaimTypes.Role) == "Vendor")
                    return await _orderService.UpdateAsync(orderDto) ? Ok("order has been updated") : BadRequest("order not updated");
                return Forbid();
            }
            else
            {
                return BadRequest("entry is not correct");
            }
        }
        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var currentUser = HttpContext.User;
                var order = await _orderService.GetByIdAsync(id);
                if (order.Basket.UserId == int.Parse(currentUser.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")) || currentUser.FindFirstValue(ClaimTypes.Role) == "Admin" || currentUser.FindFirstValue(ClaimTypes.Role) == "Vendor")
                    return await _orderService.DeleteAsync(id) ? Ok("order has been removed") : BadRequest("order not deleted");
                return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest("error when deleting an order: " + ex.Message);
            }
        }
    }
}
