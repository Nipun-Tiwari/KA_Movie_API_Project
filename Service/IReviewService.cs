using MovieManagementSystem.DTOs.Movie;
using MovieManagementSystem.DTOs.Review;

namespace MovieManagementSystem.Service
{
    public interface IReviewService
    {

        //CRUD
        Task<IEnumerable<ReviewReadDto>> GetAllReviews();
        Task<ReviewReadDto> GetReviewById(int id);
        Task<ReviewReadDto> AddReview(ReviewCreateDto Review);
        Task<bool> UpdateReview(ReviewUpdateDto Review);
        Task<bool> DeleteReview(int id);

        //Queries
        Task<IEnumerable<MovieReadDto>>  GetMoviesByMinAvgRating(double minRating);
        Task<IEnumerable<TopReviewerDto>> GetTopReviewers(int top=3);

    }
}
