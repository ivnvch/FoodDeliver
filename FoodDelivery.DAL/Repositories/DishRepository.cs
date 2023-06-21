using FoodDelivery.DAL.Entity;
using FoodDelivery.DAL.Interfaces;

namespace FoodDelivery.DAL.Repositories
{
    public class DishRepository : BaseRepository<Dish>, IDishRepository
    {

        public DishRepository(DataContext context)
            : base(context)
        {

        }

    }
}
