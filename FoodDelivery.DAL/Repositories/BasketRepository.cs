using FoodDelivery.DAL.Interfaces;
using FoodDelivery.Models.Entity;

namespace FoodDelivery.DAL.Repositories
{
    public class BasketRepository : IBaseRepository<Basket>
    {
        private readonly DataContext _context;
        public BasketRepository(DataContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Basket entity)
        {
            await _context.Baskets.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Basket entity)
        {
            _context.Baskets.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Basket> GetAllAsync()
        {
            return _context.Baskets;
        }

        public async Task<Basket> UpdateAsync(Basket entity)
        {
            _context.Baskets.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
