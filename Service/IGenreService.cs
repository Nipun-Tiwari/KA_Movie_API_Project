using MovieManagementSystem.DTOs.Genre;
using MovieManagementSystem.DTOs.Movie;

namespace MovieManagementSystem.Service
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreReadDto>> GetAllGenres();
        Task<GenreReadDto> GetGenreById(int id);
        Task<GenreReadDto> AddGenre(GenreCreateDto Genre);
        Task<bool> UpdateGenre(GenreUpdateDto Genre);
        Task<bool> DeleteGenre(int id);
    }
}
