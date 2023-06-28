using FoodDelivery.Models.ViewModel.Order;
using FoodDelivery.Service.Interfaces;
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
            return await _orderService.CreateAsync(orderDto) ? Ok("order has been created") : BadRequest("order not created");
        }
        [Authorize]
        [HttpPut("Update")]
        public async Task<IActionResult> Put(OrderDto orderDto)
        {
            try
            {
                var currentUser = HttpContext.User;
                if (_orderService.GetUserByBasketIdAsync(orderDto.BasketId).Id == int.Parse(currentUser.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")) || currentUser.FindFirstValue(ClaimTypes.Role) == "Admin" || currentUser.FindFirstValue(ClaimTypes.Role) == "Vendor")
                    return await _orderService.UpdateAsync(orderDto) ? Ok("order has been updated") : BadRequest("order not updated");
                return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest("error when changing an order " + ex.Message);
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
