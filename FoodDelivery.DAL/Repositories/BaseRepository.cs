using FoodDelivery.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FoodDelivery.DAL.Repositories
{
    public abstract class BaseRepository<T>: IBaseRepository<T> where T : class
    {
        protected DataContext _context { get; set; }

        public BaseRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<T> CreateAsync(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
           await Task.FromResult(_context.Set<T>().Remove(entity));
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
           return await Task.FromResult(_context.Set<T>().AsNoTracking());
        }

        public async Task<T> UpdateAsync(T entity)
        {
           await Task.FromResult(_context.Set<T>().Update(entity));
            return entity;
        }

        public async Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.FromResult(_context.Set<T>().Where(expression).AsNoTracking());
        }
    }
}
