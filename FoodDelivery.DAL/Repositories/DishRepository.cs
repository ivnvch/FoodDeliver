using FoodDelivery.DAL.Interfaces;
using FoodDelivery.Models.Entity;

namespace FoodDelivery.DAL.Repositories
{
    public class DishRepository : IBaseRepository<Dish>
    {
        private readonly DataContext _context;
        public DishRepository(DataContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Dish entity)
        {
            await _context.Dishes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Dish entity)
        {
            _context.Dishes.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Dish> GetAllAsync()
        {
            return _context.Dishes;
        }

        public async Task<Dish> UpdateAsync(Dish entity)
        {
            _context.Dishes.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
