using MovieManagementSystem.DTOs.Director;
using MovieManagementSystem.Models;

namespace MovieManagementSystem.Service
{
    public interface IDirectorService
    {

        //CRUD
        Task<IEnumerable<DirectorReadDto>> GetAllDirectors();
        Task<DirectorReadDto> GetDirectorById(int id);
        Task<DirectorReadDto> AddDirector(DirectorCreateDto Director);
        Task<bool> UpdateDirector(DirectorUpdateDto Director);
        Task<bool> DeleteDirector(int id);


        //Queries
        Task<DirectorReadDto> MostExperiencedDirector();
        Task<IEnumerable<DirectorReadDto>> GetActiveDirector();
        Task<IEnumerable<DirectorReadDto>> GetTopDirectorByMovieCount(int top=3);
        Task<IEnumerable<DirectorReadDto>> GetDirectorsByGenre(GenreType genre);

       
    }
}
