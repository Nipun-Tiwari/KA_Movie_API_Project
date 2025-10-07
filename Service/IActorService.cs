using MovieManagementSystem.DTOs.Actor;
using MovieManagementSystem.DTOs.Movie;

namespace MovieManagementSystem.Service
{
    public interface IActorService
    {
        //CRUD
        Task<IEnumerable<ActorReadDto>> GetAllActors();
        Task<ActorReadDto> GetActorById(int id);
        Task<ActorReadDto> AddActor(ActorCreateDto Actor);
        Task<bool> UpdateActor(ActorUpdateDto Actor);
        Task<bool> DeleteActor(int id);

        //Queries
        Task<IEnumerable<ActorReadDto>> GetAllActorsByBirth(DateTime birthDate, string beforeOrAfter);
        Task<IEnumerable<ActorUpdateDto>> ActorWithHighestWorth();
        Task<IEnumerable<ActorUpdateDto>> GetActorsInMovie(string movieName);
    }
}
