using FoodDelivery.DAL.Interfaces;
using FoodDelivery.Models.Entity;

namespace FoodDelivery.DAL.Repositories
{
    public class OrderRepository : IBaseRepository<Order>
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Order entity)
        {
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order entity)
        {
            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Order> GetAllAsync()
        {
            return _context.Orders;
        }

        public async Task<Order> UpdateAsync(Order entity)
        {
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
