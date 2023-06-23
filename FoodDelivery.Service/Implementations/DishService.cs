using FoodDelivery.DAL.Repositories;
using FoodDelivery.DAL;
using FoodDelivery.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.DAL.Interfaces;
using FoodDelivery.Models.Repsonse;
using FoodDelivery.Models.Enum;
using FoodDelivery.DAL.Entity;
using FoodDelivery.Models.ViewModel.DishViewModel;

namespace FoodDelivery.Service.Implementations
{
    public class DishService : IDishService
    {

        private readonly IBaseRepository<Dish> _dishRepo;

        public DishService(DataContext context, DishRepository dishRepo)
        {

            this._dishRepo = dishRepo;
        }



        public async Task<IBaseResponse<IEnumerable<DishViewModel>>> GetAll()
        {
            try
            {
                var dishes = await _dishRepo.GetAllAsync();
                //Select(x => new DishViewModel()
                //{
                //    Id = x.Id,
                //    Name = x.Name,
                //    Description = x.Description,
                //    Price = x.Price,
                //    Weight = x.Weight,

                //}).ToListAsync();
                return new BaseResponse<IEnumerable<DishViewModel>>()
                {
                    Data = null,
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
                var dish = await _dishRepo.GetAllAsync().FirstOrDefaultAsync(x => x.Name == model.Name);

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

                await _dishRepo.CreateAsync(dish);
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
                var dish = await _dishRepo.GetAllAsync().FirstOrDefaultAsync(x => x.Id == model.Id);
                dish.Name = model.Name;
                dish.Description = model.Description;
                dish.Price = model.Price;
                dish.Weight = model.Weight;
                dish.Category = model.Category;
                await _dishRepo.UpdateAsync(dish);
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
            var dish = await _dishRepo.GetAllAsync().FirstOrDefaultAsync(x => x.Id == id);
            await _dishRepo.DeleteAsync(dish);
            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.OK,
                Description = "Блюдо удалено",
            };
        }
    }
}

