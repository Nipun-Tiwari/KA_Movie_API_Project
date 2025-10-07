using MovieManagementSystem.DTOs.Movie;

namespace MovieManagementSystem.Service
{
    public interface IMovieService
    {

        //CRUD
        Task<IEnumerable<MovieReadDto>> GetAllMovies();
        Task<MovieReadDto> GetMovieById(int id);
        Task<MovieReadDto> AddMovie(MovieCreateDto movie);
        Task<bool> UpdateMovie(MovieUpdateDto movie);
        Task<bool> DeleteMovie(int id);


        //Queries
        Task<IEnumerable<MovieReleaseDTO>> GetMoviesByYear(int year);
        Task<IEnumerable<MovieReleaseDTO>> GetUpcomingMovies();
        Task<IEnumerable<MovieReadDto>> SearchMoviesByTitle(string keyword);   
        Task<IEnumerable<MovieReadDto>> GetMovieByActor(string actor);
        Task<IEnumerable<MovieReadDto>> GetLongestShortestMovie(string param);



    }
}
