using MovieManagementSystem.DTOs.Genre;
using MovieManagementSystem.Models;
using MovieManagementSystem.Repository;

namespace MovieManagementSystem.Service
{
    public class GenreService : IGenreService
    {
        private readonly ICRUD<Genre> _repo;

        public GenreService(ICRUD<Genre> repo)
        {
            _repo = repo;
        }

        //---------------CRUD-----------------------
        // Get all genres
        public async Task<IEnumerable<GenreReadDto>> GetAllGenres()
        {
            var genres = await _repo.GetAll();
            return genres.Select(g => new GenreReadDto
            {
                GenreId = g.GenreId,
                GenreName = g.GenreName.ToString(),
                Movies = g.MovieGenres?.Select(mg => mg.Movie.Title).ToList()
            }).ToList();
        }

        // Get genre by ID
        public async Task<GenreReadDto> GetGenreById(int id)
        {
            var genre = await _repo.GetById(id);
            return new GenreReadDto
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName.ToString(),
                Movies = genre.MovieGenres?.Select(mg => mg.Movie.Title).ToList()
            };
        }

        // Add new genre
        public async Task<GenreReadDto> AddGenre(GenreCreateDto dto)
        {
            var genre = new Genre
            {
                GenreName = dto.GenreName
            };

            var addedGenre = await _repo.Add(genre);

            return new GenreReadDto
            {
                GenreId = addedGenre.GenreId,
                GenreName = addedGenre.GenreName.ToString(),
                Movies = new List<string>()
            };
        }

        // Update genre
        public async Task<bool> UpdateGenre(GenreUpdateDto dto)
        {
            var existingGenre = await _repo.GetById(dto.GenreId);
            if (existingGenre == null)
                return false;

            existingGenre.GenreName = dto.GenreName;
            return await _repo.Update(existingGenre);
        }

        // Delete genre
        public async Task<bool> DeleteGenre(int id)
        {
            return await _repo.DeleteById(id);
        }


       

    }
}
