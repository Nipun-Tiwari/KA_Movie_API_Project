using Microsoft.AspNetCore.Mvc;
using MovieManagementSystem.DTOs.Director;
using MovieManagementSystem.Models;
using MovieManagementSystem.Service;

namespace MovieManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorService _directorService;

        public DirectorController(IDirectorService directorService)
        {
            _directorService = directorService;
        }

        //--- CRUD Endpoints ---

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectorReadDto>>> GetAllDirectors()
        {
            return Ok(await _directorService.GetAllDirectors());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DirectorReadDto>> GetDirectorById(int id)
        {
            return Ok(await _directorService.GetDirectorById(id));
        }

        [HttpPost]
        public async Task<ActionResult<DirectorReadDto>> AddDirector(DirectorCreateDto directorDto)
        {
            return Ok(await _directorService.AddDirector(directorDto));
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateDirector(DirectorUpdateDto directorDto)
        {
            return Ok(await _directorService.UpdateDirector(directorDto));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> DeleteDirector(int id)
        {
            return Ok(await _directorService.DeleteDirector(id));
        }

        //--- Query Endpoints ---

        [HttpGet("MostExperienced")]
        public async Task<ActionResult<DirectorReadDto>> MostExperiencedDirector()
        {
            return Ok(await _directorService.MostExperiencedDirector());
        }

        [HttpGet("Active")]
        public async Task<ActionResult<IEnumerable<DirectorReadDto>>> GetActiveDirector()
        {
            return Ok(await _directorService.GetActiveDirector());
        }

        [HttpGet("TopByMovieCount")]
        public async Task<ActionResult<IEnumerable<DirectorReadDto>>> GetTopDirectorByMovieCount([FromQuery] int top = 3)
        {
            return Ok(await _directorService.GetTopDirectorByMovieCount(top));
        }

        [HttpGet("ByGenre")]
        public async Task<ActionResult<IEnumerable<DirectorReadDto>>> GetDirectorsByGenre([FromQuery] GenreType genre)
        {
            return Ok(await _directorService.GetDirectorsByGenre(genre));
        }
    }
}