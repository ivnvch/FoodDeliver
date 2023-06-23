using FoodDelivery.DAL.Entity;
using FoodDelivery.Models.ViewModel.Review;

namespace FoodDelivery.Service.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetListAsync();
        Task<Review> GetByIdAsync(int id);
        Task<bool> CreateAsync(ReviewDto model);
        Task<bool> UpdateAsync(Review model);
        Task<bool> DeleteAsync(int id);
        Task<int> GetNumberReviewsAsync(int vendorId);
    }
}
