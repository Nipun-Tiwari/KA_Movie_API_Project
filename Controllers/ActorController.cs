using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagementSystem.DTOs.Actor;
using MovieManagementSystem.Service;

namespace MovieManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;
        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }


        //-------------CRUD----------------------
        [HttpGet]
        public async Task<ActionResult<ActorReadDto>> GetAllActors()
        {
            return Ok(await _actorService.GetAllActors());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ActorReadDto>> GetActorById(int id)
        {
            return Ok(await _actorService.GetActorById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ActorReadDto>> AddActor(ActorCreateDto act)
        {
            return Ok(await _actorService.AddActor(act));
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateActor(ActorUpdateDto act)
        {
            return Ok(await _actorService.UpdateActor(act));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> DeleteActor(int id)
        {
            return Ok(await _actorService.DeleteActor(id));
        }

        //-----------------Queries--------------------------

        [HttpGet("{date}/{param}")]
        public async Task<ActionResult<ActorReadDto>> GetActorsByBirth(DateTime date, string param)
        {
            return Ok(await _actorService.GetAllActorsByBirth(date, param));
        }


        [HttpGet("HighestWorth")]

        public async Task<ActionResult<IEnumerable<ActorUpdateDto>>> ActorWithHighestWorth()
        {
            return Ok(await _actorService.ActorWithHighestWorth());
        }

        [HttpGet("ByMovie")]
        public async Task<ActionResult<IEnumerable<ActorUpdateDto>>> GetActorsInMovie([FromQuery] string movieName)
        {
            return Ok(await _actorService.GetActorsInMovie(movieName));
        }






    }
}




