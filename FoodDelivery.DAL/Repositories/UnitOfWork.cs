using FoodDelivery.DAL.Interfaces;

namespace FoodDelivery.DAL.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private DataContext _context;
        private IBasketRepository _basketRepository;
        private IDishRepository _dishRepository;
        private IOrderRepository _orderRepository;
        private IProfileRepository _profileRepository;
        private IUserRepository _userRepository;

        public IBasketRepository BasketRepository
        {
            get
            {
                if (_basketRepository is null)
                {
                    _basketRepository = new BasketRepository(_context);
                }

                return _basketRepository;
            }
        }

        public IDishRepository DishRepository
        {
            get
            {
                if (_dishRepository is null)
                {
                    _dishRepository = new DishRepository(_context);
                }

                return _dishRepository;
            }
        }

        public IOrderRepository OrdersRepository
        {
            get
            {
                if (_orderRepository is null)
                {
                    _orderRepository = new OrderRepository(_context);
                }

                return _orderRepository;
            }
        }

        public IProfileRepository ProfileRepository
        {
            get
            {
                if (_profileRepository is null)
                {
                    _profileRepository = new ProfileRepository(_context);
                }

                return _profileRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository is null)
                {
                    _userRepository = new UserRepository(_context);
                }

                return _userRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
