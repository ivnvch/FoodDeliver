using FoodDelivery.DAL.Entity;
using FoodDelivery.DAL;
using FoodDelivery.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Models.ViewModel.Review;

namespace FoodDelivery.Service.Implementations
{
    public class ReviewService:IReviewService
    {
        public readonly DataContext _db;
        public ReviewService(DataContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Review>> GetListAsync()
        {
            try
            {
                var reviews = await _db.Reviews.AsNoTracking().ToListAsync();
                return reviews;
            }
            catch (Exception ex)
            {
                throw new Exception("error when getting an review list ", ex);
            }
        }
        public async Task<User> GetUserByReviewIdAsync(int reviewId)
        {
            try
            {
                int userId = (await GetByIdAsync(reviewId)).UserId;
                var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
                if (user == null)
                    throw new Exception("no user found");
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("user search error ", ex);
            }
        }
        public async Task<Review> GetByIdAsync(int id)
        {
            try
            {
                var review = await _db.Reviews.FirstOrDefaultAsync(x => x.Id == id);
                if (review == null)
                    throw new Exception("no review found");
                return review;
            }
            catch (Exception ex)
            {
                throw new Exception("review search error ", ex);
            }
        }
        public async Task<bool> CreateAsync(ReviewDto reviewDto)
        {
            try
            {
                Review review = new Review();
                review.CreationDate = reviewDto.CreationDate;
                review.UserId = reviewDto.UserId;
                review.User = await _db.Users.FirstOrDefaultAsync(x => x.Id == reviewDto.UserId);
                review.VendorId = reviewDto.VendorId;
                review.Vendor = await _db.Vendors.FirstOrDefaultAsync(x => x.Id == reviewDto.VendorId);
                review.CustomerRating = reviewDto.CustomerRating;
                review.Description = reviewDto.Description;
                _db.Reviews.Add(review);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error when creating an review ", ex);
            }
        }
        public async Task<bool> UpdateAsync(ReviewDto reviewDto)
        {
            try
            {
                var review = await GetByIdAsync(reviewDto.Id);
                review.CreationDate = reviewDto.CreationDate;
                review.UserId = reviewDto.UserId;
                review.User = await _db.Users.FirstOrDefaultAsync(x => x.Id == reviewDto.UserId);
                review.VendorId = reviewDto.VendorId;
                review.Vendor = await _db.Vendors.FirstOrDefaultAsync(x => x.Id == reviewDto.VendorId);
                review.CustomerRating = reviewDto.CustomerRating;
                review.Description = reviewDto.Description;
                _db.Update(review);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error when changing an review ", ex);
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var review = await _db.Reviews.FirstOrDefaultAsync(c => c.Id == id);
                if (review == null)
                    throw new Exception("no vendor review");
                _db.Reviews.Remove(review);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error when deleting an review ", ex);
            }
        }
        public async Task<bool> SaveAsync()
        {
            try
            {
                var saved = await _db.SaveChangesAsync();
                return saved > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception("save error ", ex);
            }
        }
    }
}
