using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagementSystem.DTOs.Actor;
using MovieManagementSystem.Service;

namespace MovieManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;
        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }


        //-------------CRUD----------------------
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<ActorReadDto>> GetAllActors()
        {
            return Ok(await _actorService.GetAllActors());
        }


        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<ActorReadDto>> GetActorById(int id)
        {
            return Ok(await _actorService.GetActorById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<ActorReadDto>> AddActor(ActorCreateDto act)
        {
            return Ok(await _actorService.AddActor(act));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<bool>> UpdateActor(ActorUpdateDto act)
        {
            return Ok(await _actorService.UpdateActor(act));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<bool>> DeleteActor(int id)
        {
            return Ok(await _actorService.DeleteActor(id));
        }

        //-----------------Queries--------------------------

        [HttpGet("{date}/{param}")]
        [Authorize(Roles = "User,Admin")]

        public async Task<ActionResult<ActorReadDto>> GetActorsByBirth(DateTime date, string param)
        {
            return Ok(await _actorService.GetAllActorsByBirth(date, param));
        }


        [HttpGet("HighestWorth")]
        [Authorize(Roles = "User,Admin")]


        public async Task<ActionResult<IEnumerable<ActorUpdateDto>>> ActorWithHighestWorth()
        {
            return Ok(await _actorService.ActorWithHighestWorth());
        }

        [HttpGet("ByMovie")]
        [Authorize(Roles = "User,Admin")]

        public async Task<ActionResult<IEnumerable<ActorUpdateDto>>> GetActorsInMovie([FromQuery] string movieName)
        {
            return Ok(await _actorService.GetActorsInMovie(movieName));
        }






    }
}




