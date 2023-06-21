using FoodDelivery.DAL.Entity;
using FoodDelivery.DAL.Interfaces;

namespace FoodDelivery.DAL.Repositories
{
    public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
    {

        public ProfileRepository(DataContext context)
            :base(context)
        {

        }
    }
}
