using FoodDelivery.DAL.Interfaces;
using FoodDelivery.DAL.Repositories;
using FoodDelivery.Service.Implementations;
using FoodDelivery.Service.Interfaces;

namespace FoodDelivery

{
    public static class Register
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IVendorRepository, VendorRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITokenService, TokenSerivce>();
            services.AddTransient<IAccountService, AccountService>();

        }
    }
}
