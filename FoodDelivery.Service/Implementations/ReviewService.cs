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
        public async Task<int> GetNumberReviewsAsync(int vendorId)
        {
            try
            {
                Vendor vendor = await _db.Vendors.FirstOrDefaultAsync(x => x.Id == vendorId);
                if (vendor == null)
                    throw new Exception("no vendor found ");
                int numberReviews = vendor.Reviews.Count;
                return numberReviews == 0 ? 0 : numberReviews;
            }
            catch (Exception ex)
            {
                throw new Exception("error while getting number of reviews ", ex);
            }
        }
        public async Task<IEnumerable<Review>> GetListAsync()
        {
            try
            {
                var review = await _db.Reviews.AsNoTracking().ToListAsync();
                return review;
            }
            catch (Exception ex)
            {
                throw new Exception("error when getting an review list ", ex);
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
        public async Task<bool> UpdateAsync(Review review)
        {
            try
            {
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
