using FoodDelivery.DAL.Entity;
using FoodDelivery.DAL.Interfaces;

namespace FoodDelivery.DAL.Repositories
{
    public class VendorRepository : BaseRepository<Vendor>, IVendorRepository
    {
        public VendorRepository(DataContext context)
            : base(context)
        {

        }
    }
}
