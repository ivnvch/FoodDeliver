using FoodDelivery.DAL.Interfaces;
using FoodDelivery.DAL.Repositories;
using FoodDelivery.Models.Entity;
using FoodDelivery.Service.Implementations;
using FoodDelivery.Service.Interfaces;

namespace FoodDelivery
{ 
    public static class Register
    {

        public static void RegisterRepositories(this IServiceCollection services)
        {
           services.AddScoped<IBaseRepository<User>, UserRepository>();
           services.AddScoped<IBaseRepository<Basket>, BasketRepository>();
           services.AddScoped<IBaseRepository<Dish>, DishRepository>();
           services.AddScoped<IBaseRepository<Order>, OrderRepository>();
           services.AddScoped<IBaseRepository<Profile>, ProfileRepository>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
