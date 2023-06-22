using FoodDelivery.DAL.Repositories;
using FoodDelivery.DAL;

using FoodDelivery.Models.Entity;
using FoodDelivery.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.DAL.Interfaces;
using FoodDelivery.Models.Repsonse;
using FoodDelivery.Models.ViewModel.DishViewModel;
using FoodDelivery.Models.Enum;
using FoodDelivery.Models.ViewModel.User;
using FoodDelivery.DAL.Entity;
using FoodDelivery.Models.Entity.DTO;

namespace FoodDelivery.Service.Implementations
{
    public class DishService : IDishService
    {
      
        private readonly IUnitOfWork _unitOfWork;

        public DishService(IUnitOfWork unitOfWork)
        {            
            _unitOfWork = unitOfWork;
        }



        public async Task<IBaseResponse<IEnumerable<DishViewModel>>> GetAll()
        {
            try
            {
                var dishs = await _unitOfWork.DishRepository.GetAllAsync(); 
                List<DishViewModel> dishsView = await dishs.Select(x => new DishViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Weight = x.Weight,
                }).ToListAsync();
                return new BaseResponse<IEnumerable<DishViewModel>>()
                {
                    Data = dishsView,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<DishViewModel>>()
                {
                    Description = $"Внутренняя ошибка {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<Dish>> Create(DishCreateDTO model)
        {
            try
            {
                Dish dish = (Dish)await _unitOfWork.DishRepository.FindByConditionAsync(x => x.Name == model.Name);

                if (dish != null)
                {
                    return new BaseResponse<Dish>()
                    {
                        Description = "Такое блюдо уже имеется",
                        StatusCode = Models.Enum.StatusCode.DishAlreadyExist,
                    };
                }

                dish = new Dish()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Weight = model.Weight,
                    Category = model.Category,
                };

                await _unitOfWork.DishRepository.CreateAsync(dish);
                await _unitOfWork.SaveAsync();
                return new BaseResponse<Dish>()
                {
                    Data = dish,
                    Description = "Блюдо Создано",
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dish>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутреняя ошибка {ex.Message}"
                };
            }

        }

        public async Task<IBaseResponse<bool>> Update(DishUpdateDTO model)
        {
            try
            {
                Dish dish = (Dish)await _unitOfWork.DishRepository.FindByConditionAsync(x => x.Id == model.Id);
                dish.Name = model.Name;
                dish.Description = model.Description;
                dish.Price = model.Price;
                dish.Weight = model.Weight;
                dish.Category = model.Category;

                await _unitOfWork.DishRepository.UpdateAsync(dish);
                await _unitOfWork.SaveAsync();
                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                    Description = "Блюдо обновлено",
                };
            }
             catch (Exception ex)
            {
                throw new Exception("ошибка при обновлении блюда ", ex);
            }

        }


        public async Task<IBaseResponse<bool>> Delete(int id)
        {           
            Dish dish = (Dish)await _unitOfWork.DishRepository.FindByConditionAsync(x => x.Id == id);
            await _unitOfWork.DishRepository.DeleteAsync(dish);
            await _unitOfWork.SaveAsync();
            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.OK,
                Description = "Блюдо удалено",
            };
        }
    }
}

