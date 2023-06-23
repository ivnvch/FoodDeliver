using FoodDelivery.DAL.Entity;
using FoodDelivery.DAL.Interfaces;

namespace FoodDelivery.DAL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context)
            : base(context)
        {

        }        
    }
}
