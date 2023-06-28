using FoodDelivery.DAL;
using FoodDelivery.DAL.Entity;
using FoodDelivery.Models.ViewModel.Order;
using FoodDelivery.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Service.Implementations
{
    public class OrderService : IOrderService
    {
        public readonly DataContext _db;
        public OrderService(DataContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Order>> GetListAsync()
        {
            try
            {
                var orders = await _db.Orders.AsNoTracking().ToListAsync();
                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception("error when getting an order list ", ex);
            }
        }
        public async Task<Order> GetByIdAsync(int id)
        {
            try
            {
                var order = await _db.Orders.FirstOrDefaultAsync(x => x.Id == id);
                if (order == null)
                    throw new Exception("no order found");
                return order;
            }
            catch (Exception ex)
            {
                throw new Exception("order search error ", ex);
            }
        }
        public async Task<bool> CreateAsync(OrderDto orderDto)
        {
            try
            {
                Order order = new Order();
                order.DateCreate = orderDto.DateCreate;
                order.Price = orderDto.Price;
                order.IsComplete = orderDto.IsComplete;
                order.BasketId = orderDto.BasketId;
                order.Basket = await _db.Baskets.FirstOrDefaultAsync(x => x.Id == order.BasketId);
                order.Address = orderDto.Address;
                order.Commentary = orderDto.Commentary;
                _db.Orders.Add(order);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error when creating an order ", ex);
            }
        }
        public async Task<bool> UpdateAsync(OrderDto orderDto)
        {
            try
            {
                var order = await GetByIdAsync(orderDto.Id);
                order.DateCreate = orderDto.DateCreate;
                order.Price = orderDto.Price;
                order.IsComplete = orderDto.IsComplete;
                order.BasketId = orderDto.BasketId;
                order.Basket = await GetBasketAsync(order.BasketId);
                order.Address = orderDto.Address;
                order.Commentary = orderDto.Commentary;
                _db.Update(order);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error when changing an order ", ex);
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var order = await _db.Orders.FirstOrDefaultAsync(c => c.Id == id);
                if (order == null)
                    throw new Exception("no order found");
                _db.Orders.Remove(order);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error when deleting an order ", ex);
            }
        }
        public async Task<Basket> GetBasketAsync(int id)
        {
            try
            {
                var basket = await _db.Baskets.FindAsync(id);
                if (basket == null)
                    throw new Exception("no basket found");
                return basket;
            }
            catch (Exception ex)
            {
                throw new Exception("basket search error ", ex);
            }
        }
        public async Task<User> GetUserByBasketIdAsync(int basketId)
        {
            try
            {
                var basket = await GetBasketAsync(basketId);
                var user = basket.User;
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("user search error ", ex);
            }
        }
        public async Task<bool> SaveAsync()
        {
            try
            {
                var saved = await _db.SaveChangesAsync();
                return saved > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception("save error ", ex);
            }
        }
    }
}