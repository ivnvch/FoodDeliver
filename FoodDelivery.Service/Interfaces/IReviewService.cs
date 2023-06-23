using FoodDelivery.DAL.Entity;
using FoodDelivery.Models.ViewModel.Review;

namespace FoodDelivery.Service.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetListAsync();
        Task<Review> GetByIdAsync(int id);
        Task<User> GetUserByReviewIdAsync(int reviewId);
        Task<bool> CreateAsync(ReviewDto model);
        Task<bool> UpdateAsync(ReviewDto model);
        Task<bool> DeleteAsync(int id);
    }
}
