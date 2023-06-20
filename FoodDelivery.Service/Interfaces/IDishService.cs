using FoodDelivery.Models.Entity;
using FoodDelivery.Models.Entity.DTOs;
using FoodDelivery.Models.Repsonse;
using FoodDelivery.Models.ViewModel.DishViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Service.Interfaces
{
    public interface IDishService
    {
        Task<IBaseResponse<Dish>> Create(DishCreateDTO dishCreateDTO);
        Task<IBaseResponse<bool>> Update(DishUpdateDTO dishUpdateDTO);
        Task<IBaseResponse<bool>> Delete(int id);
        Task<IBaseResponse<IEnumerable<DishViewModel>>> GetAll();     
    }
}
