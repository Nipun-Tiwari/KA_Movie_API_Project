using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieManagementSystem.DTOs.Movie;
using MovieManagementSystem.DTOs.Review;
using MovieManagementSystem.Service;

namespace MovieManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        //--- CRUD Endpoints ---

        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<ReviewReadDto>>> GetAllReviews()
        {
            return Ok(await _reviewService.GetAllReviews());
        }

        [HttpGet("{id:int}")]
        [Authorize]

        public async Task<ActionResult<ReviewReadDto>> GetReviewById(int id)
        {
            return Ok(await _reviewService.GetReviewById(id));
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<ReviewReadDto>> AddReview(ReviewCreateDto reviewDto)
        {
            return Ok(await _reviewService.AddReview(reviewDto));
        }

        [HttpPut]
        [Authorize]

        public async Task<ActionResult<bool>> UpdateReview(ReviewUpdateDto reviewDto)
        {
            return Ok(await _reviewService.UpdateReview(reviewDto));
        }

        [HttpDelete("{id:int}")]
        [Authorize]

        public async Task<ActionResult<bool>> DeleteReview(int id)
        {
            return Ok(await _reviewService.DeleteReview(id));
        }

        //--- Query Endpoints ---

        [HttpGet("ByMinRating")]
        [Authorize(Roles = "User,Admin")]

        public async Task<ActionResult<IEnumerable<MovieReadDto>>> GetMoviesByMinAvgRating([FromQuery] double avgRating)
        {
            return Ok(await _reviewService.GetMoviesByMinAvgRating(avgRating));
        }

        [HttpGet("TopReviewers")]
        [Authorize(Roles = "User,Admin")]

        public async Task<ActionResult<IEnumerable<TopReviewerDto>>> GetTopReviewers([FromQuery] int top = 3)
        {
            return Ok(await _reviewService.GetTopReviewers(top));
        }
    }
}