using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieManagementSystem.DTOs.Genre;
using MovieManagementSystem.Service;

namespace MovieManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        //--- CRUD Endpoints ---

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<IEnumerable<GenreReadDto>>> GetAllGenres()
        {
            return Ok(await _genreService.GetAllGenres());
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<GenreReadDto>> GetGenreById(int id)
        {
            return Ok(await _genreService.GetGenreById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<GenreReadDto>> AddGenre(GenreCreateDto genreDto)
        {
            return Ok(await _genreService.AddGenre(genreDto));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<bool>> UpdateGenre(GenreUpdateDto genreDto)
        {
            return Ok(await _genreService.UpdateGenre(genreDto));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<bool>> DeleteGenre(int id)
        {
            return Ok(await _genreService.DeleteGenre(id));
        }
    }
}