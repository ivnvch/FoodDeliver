using FoodDelivery.DAL.Interfaces;
using FoodDelivery.Models.Entity;


namespace FoodDelivery.DAL.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<User> GetAllAsync()
        {
            return _context.Users;
        }

        public async Task<User> UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
