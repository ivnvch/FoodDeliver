using Azure;
using FoodDelivery.Models.Entity.DTOs;
using FoodDelivery.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    public class DishController : Controller
    {
       
        private readonly IDishService _dishService;


        public DishController(IDishService dishService)
        {
            this._dishService = dishService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDishes()
        {
            var response = await _dishService.GetAll();
            if (response.StatusCode == Models.Enum.StatusCode.OK)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDishes(DishUpdateDTO dish)
        {
            if (ModelState.IsValid)
            {
                var responce = await _dishService.Update(dish);
                return RedirectToAction("List");
            }
            else
            {
                return View(dish);
            }
        }
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDishes(DishCreateDTO entity)
        {
            if (ModelState.IsValid)
            {
               var dish = await _dishService.Create(entity);
                return RedirectToAction("List");
            }
            else
            {
                return View(entity);
            }

        }
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDish(int id)
        {

            var responce = await _dishService.Delete(id);
            return RedirectToAction("List");

        }
    }
}
