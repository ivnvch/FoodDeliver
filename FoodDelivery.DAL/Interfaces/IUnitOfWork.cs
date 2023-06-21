

namespace FoodDelivery.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IBasketRepository BasketRepository { get; }
        IDishRepository DishRepository { get; }
        IOrderRepository OrdersRepository { get; }
        IProfileRepository ProfileRepository { get; }
        IUserRepository UserRepository { get; }
        Task SaveAsync();
    }
}
