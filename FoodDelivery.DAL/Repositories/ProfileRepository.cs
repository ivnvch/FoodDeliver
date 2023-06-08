using FoodDelivery.DAL.Interfaces;
using FoodDelivery.Models.Entity;

namespace FoodDelivery.DAL.Repositories
{
    public class ProfileRepository : IBaseRepository<Profile>
    {
        private readonly DataContext _context;

        public ProfileRepository(DataContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Profile entity)
        {
            await _context.Profiles.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Profile entity)
        {
            _context.Profiles.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Profile> GetAllAsync()
        {
            return _context.Profiles;
        }

        public async Task<Profile> UpdateAsync(Profile entity)
        {
            _context.Profiles.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
