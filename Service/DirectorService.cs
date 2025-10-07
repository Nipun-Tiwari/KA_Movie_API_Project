using MovieManagementSystem.DTOs.Director;
using MovieManagementSystem.Models;
using MovieManagementSystem.Repository;

namespace MovieManagementSystem.Service
{
    public class DirectorService : IDirectorService
    {
        private readonly ICRUD<Director> _repo;

        public DirectorService(ICRUD<Director> repo)
        {
            _repo = repo;
        }

        //---------------CRUD--------------------------
        // Get all directors
        public async Task<IEnumerable<DirectorReadDto>> GetAllDirectors()
        {
            var directors = await _repo.GetAll();
            return directors.Select(d => new DirectorReadDto
            {
                DirectorId = d.DirectorId,
                DirectorName = d.DirectorName,
                YearsOfExperience = d.YearsOfExperience,
                IsActive = d.IsActive,
                Email = d.Email,
                Movies = d.Movies?.Select(m => m.Title).ToList()
            }).ToList();
        }

        // Get director by ID
        public async Task<DirectorReadDto> GetDirectorById(int id)
        {
            var director = await _repo.GetById(id);
            return new DirectorReadDto
            {
                DirectorId = director.DirectorId,
                DirectorName = director.DirectorName,
                YearsOfExperience = director.YearsOfExperience,
                IsActive = director.IsActive,
                Email = director.Email,
                Movies = director.Movies?.Select(m => m.Title).ToList()
            };
        }

        // Add a new director
        public async Task<DirectorReadDto> AddDirector(DirectorCreateDto dto)
        {
            var director = new Director
            {
                DirectorName = dto.DirectorName,
                YearsOfExperience = dto.YearsOfExperience,
                IsActive = dto.IsActive,
                Email = dto.Email
            };

            var addedDirector = await _repo.Add(director);

            return new DirectorReadDto
            {
                DirectorId = addedDirector.DirectorId,
                DirectorName = addedDirector.DirectorName,
                YearsOfExperience = addedDirector.YearsOfExperience,
                IsActive = addedDirector.IsActive,
                Email = addedDirector.Email,
                Movies = new List<string>()
            };
        }

        // Update a director
        public async Task<bool> UpdateDirector(DirectorUpdateDto dto)
        {
            var existingDirector = await _repo.GetById(dto.DirectorId);
            if (existingDirector == null)
                return false;

            existingDirector.DirectorName = dto.DirectorName;
            existingDirector.YearsOfExperience = dto.YearsOfExperience;
            existingDirector.IsActive = dto.IsActive;
            existingDirector.Email = dto.Email;

            return await _repo.Update(existingDirector);
        }

        // Delete a director
        public async Task<bool> DeleteDirector(int id)
        {
            return await _repo.DeleteById(id);
        }


        //-------------------Queries---------------------------------
        public async Task<DirectorReadDto> MostExperiencedDirector()
        {
            var directors = await _repo.GetAll();
            var mostExperienced = directors
                .OrderByDescending(d => d.YearsOfExperience)
                .FirstOrDefault();

            if (mostExperienced == null)
                throw new Exception("No directors found");

            return new DirectorReadDto
            {
                DirectorId = mostExperienced.DirectorId,
                DirectorName = mostExperienced.DirectorName,
                YearsOfExperience = mostExperienced.YearsOfExperience,
                IsActive = mostExperienced.IsActive,
                Email = mostExperienced.Email,
                Movies = mostExperienced.Movies?.Select(m => m.Title).ToList()
            };
        }


        public async Task<IEnumerable<DirectorReadDto>> GetActiveDirector()
        {
            var allDirectors = await _repo.GetAll();

            var activeDirectors = allDirectors
                    .Where(d => d.IsActive == true)
                    .Select(d => new DirectorReadDto
                    {
                        DirectorId = d.DirectorId,
                        DirectorName = d.DirectorName,
                        YearsOfExperience = d.YearsOfExperience,
                        IsActive = d.IsActive,
                        Email = d.Email,
                        Movies = d.Movies?.Select(m => m.Title).ToList()
                    })
                    .ToList();
            return activeDirectors;
        }


        public async Task<IEnumerable<DirectorReadDto>> GetTopDirectorByMovieCount(int top = 3)
        {
            var directors = await _repo.GetAll();

            var topDirector = directors
                .Where(a => a.Movies != null && a.Movies.Any())
                .OrderByDescending(d => d.Movies!.Count())
                .Select(d => new DirectorReadDto
                {
                    DirectorId = d.DirectorId,
                    DirectorName = d.DirectorName,
                    Email = d.Email,
                    YearsOfExperience = d.YearsOfExperience,
                    Movies = d.Movies?.Select(a => a.Title).ToList()
                }).ToList();
            return topDirector;
               
        }

        public async Task<IEnumerable<DirectorReadDto>> GetDirectorsByGenre(GenreType genre)
        {
            var directors = await _repo.GetAll();

            var filteredDirect = directors
                .Where(d => d.Movies != null && d.Movies.Any(m => m.MovieGenres!.Any(p => p.Genre!.GenreName == genre)))
                .Select(d => new DirectorReadDto
                {
                    DirectorId = d.DirectorId,
                    DirectorName = d.DirectorName,
                    YearsOfExperience = d.YearsOfExperience,
                    IsActive = d.IsActive,
                    Email = d.Email,
                    Movies = d.Movies?
                        .Where(m => m.MovieGenres!
                            .Any(mg => mg.Genre!.GenreName == genre))
                    .Select(m => m.Title)
                    .ToList()
                })
                .ToList();
                
            return filteredDirect;
        }



    }
}
