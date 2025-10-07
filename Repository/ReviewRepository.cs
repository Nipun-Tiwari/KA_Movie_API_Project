using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.Data;
using MovieManagementSystem.Models;

namespace MovieManagementSystem.Repository
{
    public class ReviewRepository: ICRUD<Review>
    {
        private readonly ManagementContext _context;

        public ReviewRepository(ManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAll()
        {
            return await _context.Reviews
                .Include(r => r.Movie)
                .ToListAsync();
        }

        public async Task<Review> GetById(int id)
        {
            return await _context.Reviews
                .Include(r => r.Movie)
                .FirstOrDefaultAsync(r => r.ReviewId == id)
                ?? throw new Exception("Review not found");
        }

        public async Task<Review> Add(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<bool> Update(Review review)
        {
            var existingReview = await _context.Reviews.FindAsync(review.ReviewId);
            if (existingReview == null)
                return false;

            _context.Entry(existingReview).CurrentValues.SetValues(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteById(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                return false;

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
