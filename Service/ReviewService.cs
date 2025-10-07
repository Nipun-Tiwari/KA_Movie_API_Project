using MovieManagementSystem.DTOs.Review;
using MovieManagementSystem.Models;
using MovieManagementSystem.Repository;
using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.DTOs.Movie;

namespace MovieManagementSystem.Service
{
    public class ReviewService : IReviewService
    {
        private readonly ICRUD<Review> _repo;

        public ReviewService(ICRUD<Review> repo)
        {
            _repo = repo;
        }

        // Get all reviews
        public async Task<IEnumerable<ReviewReadDto>> GetAllReviews()
        {
            var reviews = await _repo.GetAll();
            return reviews.Select(r => new ReviewReadDto
            {
                ReviewId = r.ReviewId,
                ReviewerName = r.ReviewerName,
                Comment = r.Comment,
                Stars = r.Stars,
                MovieTitle = r.Movie?.Title ?? "Unknown"
            }).ToList();
        }


        //-------------CRUD-------------------

        // Get review by ID
        public async Task<ReviewReadDto> GetReviewById(int id)
        {
            var review = await _repo.GetById(id);
            return new ReviewReadDto
            {
                ReviewId = review.ReviewId,
                ReviewerName = review.ReviewerName,
                Comment = review.Comment,
                Stars = review.Stars,
                MovieTitle = review.Movie?.Title ?? "Unknown"
            };
        }

        // Add new review
        public async Task<ReviewReadDto> AddReview(ReviewCreateDto dto)
        {
            var review = new Review
            {
                MovieId = dto.MovieId,
                ReviewerName = dto.ReviewerName,
                Comment = dto.Comment,
                Stars = dto.Stars
            };

            var addedReview = await _repo.Add(review);

            return new ReviewReadDto
            {
                ReviewId = addedReview.ReviewId,
                ReviewerName = addedReview.ReviewerName,
                Comment = addedReview.Comment,
                Stars = addedReview.Stars,
                MovieTitle = addedReview.Movie?.Title ?? "Unknown"
            };
        }

        // Update review
        public async Task<bool> UpdateReview(ReviewUpdateDto dto)
        {
            var existingReview = await _repo.GetById(dto.ReviewId);
            if (existingReview == null)
                return false;

            existingReview.Comment = dto.Comment;
            existingReview.Stars = dto.Stars;

            return await _repo.Update(existingReview);
        }

        // Delete review
        public async Task<bool> DeleteReview(int id)
        {
            return await _repo.DeleteById(id);
        }


        //-------------Queries-------------------------


        public async Task<IEnumerable<MovieReadDto>> GetMoviesByMinAvgRating(double avgRating)
        {
            var reviews = await _repo.GetAll();

            var moviesWithAvg = reviews.GroupBy(m => m.Movie)
                .Select(g => new
                {
                    Movie = g.Key,
                    AvgRating = g.Average(r => r.Stars)
                })
                .Where(m => m.AvgRating >= avgRating)
                .Select(o => new MovieReadDto
                {
                    MovieId = o.Movie.MovieId,
                    Title = o.Movie.Title,
                    ReleaseDate = o.Movie.ReleaseDate,
                    IsReleased = o.Movie.isReleased,
                    DirectorName = o.Movie.Director?.DirectorName,
                    GenreNames = o.Movie.MovieGenres?
                        .Select(a => a.Genre.GenreName.ToString())
                        .ToList(),
                    AverageRating = o.AvgRating
                })
                .ToList();
            return moviesWithAvg;
            
        }

        public async Task<IEnumerable<TopReviewerDto>> GetTopReviewers(int top = 3)
        {
            var reviews = await _repo.GetAll();

            var topReviewers = reviews
                .GroupBy(r => r.ReviewerName)
                .Select(g => new TopReviewerDto
                {
                    ReviewerName = g.Key,
                    ReviewCount = g.Count()
                })
                .OrderByDescending(a => a.ReviewCount)
                .Take(top)
                .ToList();
            return topReviewers;

        }


    }
}
