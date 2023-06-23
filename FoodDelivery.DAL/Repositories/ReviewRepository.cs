using FoodDelivery.DAL.Entity;
using FoodDelivery.DAL.Interfaces;

namespace FoodDelivery.DAL.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(DataContext context)
           : base(context)
        {

        }
    }
}
