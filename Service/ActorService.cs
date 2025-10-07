using Microsoft.Identity.Client;
using MovieManagementSystem.DTOs.Actor;
using MovieManagementSystem.Models;
using MovieManagementSystem.Repository;

namespace MovieManagementSystem.Service
{
    public class ActorService : IActorService
    {
        private readonly ICRUD<Actor> _repo;
        public ActorService(ICRUD<Actor> repo)
        {
            _repo = repo;
        }


        //---------------------CRUD-----------------------------
        public async Task<IEnumerable<ActorReadDto>> GetAllActors()
        {
            var actors = await _repo.GetAll();
            return actors.Select(x => new ActorReadDto
            {
                ActorId = x.ActorId,
                ActorName = x.ActorName,
                Gender = x.Gender.ToString(),
                BirthDate = x.BirthDate,
                Movies = x.MovieActors?.Select(a => a.Movie.Title).ToList()
            }).ToList();
        }

        public async Task<ActorReadDto> GetActorById(int id)
        {
            var actor = await _repo.GetById(id);
            return new ActorReadDto
            {
                ActorId = actor.ActorId,
                ActorName = actor.ActorName,
                Gender = actor.Gender.ToString(),
                BirthDate = actor.BirthDate,
                Movies = actor.MovieActors?.Select(a => a.Movie.Title).ToList()
            };
        }


        public async Task<ActorReadDto> AddActor(ActorCreateDto act)
        {
            var actor = new Actor
            {
                ActorName = act.ActorName,
                BirthDate = act.BirthDate,
                Gender = act.Gender,
                NetWorth = act.NetWorth
            };

            var addedActor = await _repo.Add(actor);
            return new ActorReadDto
            {
                ActorId = addedActor.ActorId,
                ActorName = addedActor.ActorName,
                Gender = addedActor.Gender.ToString(),
                BirthDate = addedActor.BirthDate,
                Movies = addedActor.MovieActors?.Select(a => a.Movie.Title).ToList()
            };

        }
        public async Task<bool> UpdateActor(ActorUpdateDto act)
        {
            var existingActor = await _repo.GetById(act.ActorId);
            if (existingActor == null)
            {
                return false;
            }
            existingActor.ActorName = act.ActorName;
            existingActor.BirthDate = act.BirthDate;
            existingActor.Gender = act.Gender;
            existingActor.NetWorth = act.NetWorth;

            return await _repo.Update(existingActor);

        }

        public async Task<bool> DeleteActor(int id)
        {
            return await _repo.DeleteById(id);
        }


        //--------------Queries--------------------------------

        public async Task<IEnumerable<ActorReadDto>> GetAllActorsByBirth(DateTime birthDate, string beforeOrAfter)
        {
            string filter = beforeOrAfter.ToLower();
            var actors = await _repo.GetAll();
            IEnumerable<Actor> actor;
            if (filter == "after")
            {
                actor = actors.Where(a => a.BirthDate >= birthDate).ToList();
            }

            else
            {
                actor = actors.Where(a => a.BirthDate <= birthDate).ToList();
            }

            return actor.Select(a => new ActorReadDto
            {
                ActorId = a.ActorId,
                ActorName = a.ActorName,
                BirthDate = a.BirthDate,
                Gender = a.Gender.ToString(),
                Movies = a.MovieActors?.Select(ma => ma.Movie.Title).ToList()
            }).ToList();

        }


        public async Task<IEnumerable<ActorUpdateDto>> ActorWithHighestWorth()
        {
            var actors=await _repo.GetAll();
            var actorHighestWorth = actors
                .OrderByDescending(a => a.NetWorth)
                .Take(1);

            return actorHighestWorth.Select(a => new ActorUpdateDto
            {
                ActorId = a.ActorId,
                ActorName = a.ActorName,
                Gender = a.Gender,
                NetWorth = a.NetWorth,
                BirthDate = a.BirthDate
            });
        }

        public async Task<IEnumerable<ActorUpdateDto>> GetActorsInMovie(string movieName)
        {
            var actors= await _repo.GetAll();
            var actorsInMovie = actors.Where(a => a.MovieActors != null && 
                a.MovieActors.Any(b => b.Movie.Title.ToLower() == movieName.ToLower()));

            return actorsInMovie.Select(a => new ActorUpdateDto
            {
                ActorId = a.ActorId,
                ActorName = a.ActorName,
                BirthDate = a.BirthDate,
                Gender = a.Gender,
                NetWorth = a.NetWorth

            });
        }




    }
}
