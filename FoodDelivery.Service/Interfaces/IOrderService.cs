using FoodDelivery.DAL.Entity;
using FoodDelivery.Models.ViewModel.DTOs;

namespace FoodDelivery.Service.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetListAsync();
        Task<Order> GetByIdAsync(int id);
        Task<bool> CreateAsync(OrderDto model);
        Task<bool> UpdateAsync(Order model);
        Task<bool> DeleteAsync(int id);
        Task<Basket> GetBasketAsync(int id);
        Task<User> GetUserByBasketIdAsync(int id);
    }
}
