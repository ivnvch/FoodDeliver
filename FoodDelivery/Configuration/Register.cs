using FoodDelivery.DAL.Entity;
using FoodDelivery.DAL.Interfaces;
using FoodDelivery.DAL.Repositories;
using FoodDelivery.Service.Implementations;
using FoodDelivery.Service.Interfaces;

namespace FoodDelivery.Configuration
{
    public static class Register
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<User>, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITokenService, TokenSerivce>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IVendorService, VendorService>();
            services.AddTransient<IDishService, DishService>();
        }
    }
}
