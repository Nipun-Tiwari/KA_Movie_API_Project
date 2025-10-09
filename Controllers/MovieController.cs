using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieManagementSystem.DTOs.Movie;
using MovieManagementSystem.Service;

namespace MovieManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        //--- CRUD Endpoints ---

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<IEnumerable<MovieReadDto>>> GetAllMovies()
        {
            return Ok(await _movieService.GetAllMovies());
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<MovieReadDto>> GetMovieById(int id)
        {
            return Ok(await _movieService.GetMovieById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<MovieReadDto>> AddMovie(MovieCreateDto movieDto)
        {
            return Ok(await _movieService.AddMovie(movieDto));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<bool>> UpdateMovie(MovieUpdateDto movieDto)
        {
            return Ok(await _movieService.UpdateMovie(movieDto));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<bool>> DeleteMovie(int id)
        {
            return Ok(await _movieService.DeleteMovie(id));
        }

        //--- Query Endpoints ---

        [HttpGet("ByYear/{year:int}")]
        [Authorize(Roles = "User,Admin")]

        public async Task<ActionResult<IEnumerable<MovieReleaseDTO>>> GetMoviesByYear(int year)
        {
            return Ok(await _movieService.GetMoviesByYear(year));
        }

        [HttpGet("Upcoming")]
        [Authorize(Roles = "User,Admin")]

        public async Task<ActionResult<IEnumerable<MovieReleaseDTO>>> GetUpcomingMovies()
        {
            return Ok(await _movieService.GetUpcomingMovies());
        }

        [HttpGet("SearchByTitle")]
        [Authorize(Roles = "User,Admin")]

        public async Task<ActionResult<IEnumerable<MovieReadDto>>> SearchMoviesByTitle([FromQuery] string keyword)
        {
            return Ok(await _movieService.SearchMoviesByTitle(keyword));
        }

        [HttpGet("ByActor")]
        [Authorize(Roles = "User,Admin")]

        public async Task<ActionResult<IEnumerable<MovieReadDto>>> GetMovieByActor([FromQuery] string actor)
        {
            return Ok(await _movieService.GetMovieByActor(actor));
        }

        [HttpGet("Duration/{param}")]
        [Authorize(Roles = "User,Admin")]

        public async Task<ActionResult<IEnumerable<MovieReadDto>>> GetLongestShortestMovie(string param)
        {
            return Ok(await _movieService.GetLongestShortestMovie(param));
        }
    }
}