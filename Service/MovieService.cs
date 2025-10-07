using MovieManagementSystem.DTOs.Movie;
using MovieManagementSystem.Models;
using MovieManagementSystem.Repository;
using System.Collections.Generic;

namespace MovieManagementSystem.Service
{
    public class MovieService : IMovieService
    {
        private readonly ICRUD<Movie> _repo;

        public MovieService(ICRUD<Movie> repo)
        {
            _repo = repo;
        }


        //--------------------CRUD-------------------------
        // Get all movies
        public async Task<IEnumerable<MovieReadDto>> GetAllMovies()
        {
            var movies = await _repo.GetAll();
            return movies.Select(m => new MovieReadDto
            {
                MovieId = m.MovieId,
                Title = m.Title,
                ReleaseDate = m.ReleaseDate,
                IsReleased = m.isReleased,
                DirectorName = m.Director?.DirectorName,
                GenreNames = m.MovieGenres?
                        .Select(a => a.Genre.GenreName.ToString())
                        .ToList(),
                AverageRating = m.Reviews != null && m.Reviews.Any() ? m.Reviews.Average(r => r.Stars) : 0
            }).ToList();
        }

        // Get movie by ID
        public async Task<MovieReadDto> GetMovieById(int id)
        {
            var movie = await _repo.GetById(id);
            if (movie == null)
                throw new KeyNotFoundException("Doesn't Exist");

            return new MovieReadDto
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                IsReleased = movie.isReleased,
                DirectorName = movie.Director?.DirectorName,
                GenreNames = movie.MovieGenres?
                        .Select(a => a.Genre.GenreName.ToString())
                        .ToList(),
                AverageRating = movie.Reviews != null && movie.Reviews.Any() ? movie.Reviews.Average(r => r.Stars) : 0
            };
        }

        // Add a new movie
        public async Task<MovieReadDto> AddMovie(MovieCreateDto dto)
        {
            var movie = new Movie
            {
                Title = dto.Title,
                ReleaseDate = dto.ReleaseDate,
                Duration = dto.Duration,
                isReleased = dto.IsReleased,
                DirectorId = dto.DirectorId
            };

            var addedMovie = await _repo.Add(movie);

            return new MovieReadDto
            {
                MovieId = addedMovie.MovieId,
                Title = addedMovie.Title,
                ReleaseDate = addedMovie.ReleaseDate,
                IsReleased = addedMovie.isReleased,
                DirectorName = addedMovie.Director?.DirectorName,
                GenreNames = addedMovie.MovieGenres?
                        .Select(a => a.Genre.GenreName.ToString())
                        .ToList(),
                AverageRating = 0
            };
        }

        // Update existing movie
        public async Task<bool> UpdateMovie(MovieUpdateDto dto)
        {
            var existingMovie = await _repo.GetById(dto.MovieId);
            if (existingMovie == null)
                return false;

            existingMovie.Title = dto.Title;
            existingMovie.ReleaseDate = dto.ReleaseDate;
            existingMovie.Duration = dto.Duration;
            existingMovie.isReleased = dto.IsReleased;
            existingMovie.DirectorId = dto.DirectorId;

            return await _repo.Update(existingMovie);
        }

        // Delete a movie
        public async Task<bool> DeleteMovie(int id)
        {
            return await _repo.DeleteById(id);
        }



        //-------------------------QUeries-------------------------


        public async Task<IEnumerable<MovieReleaseDTO>> GetMoviesByYear(int year)
        {
            var movies = await _repo.GetAll();

            var filteredYear = movies
                .Where(a => a.isReleased && a.ReleaseDate.Year == year)
                .Select(b => new MovieReleaseDTO
                {
                    Title = b.Title,
                    ReleaseDate = b.ReleaseDate,
                    DirectorName = b.Director?.DirectorName,
                    GenreNames = b.MovieGenres?
                        .Select(a => a.Genre.GenreName.ToString())
                        .ToList()
                })
                .ToList();

            return filteredYear;
        }

        public async Task<IEnumerable<MovieReleaseDTO>> GetUpcomingMovies()
        {
            DateTime curDateTime = DateTime.Now;
            var movies = await _repo.GetAll();
            var upcomingMovies = movies.Where(a => a.ReleaseDate > curDateTime)
                .Select(a => new MovieReleaseDTO
                {
                    Title = a.Title,
                    ReleaseDate = a.ReleaseDate,
                    DirectorName = a.Director?.DirectorName,
                    GenreNames = a.MovieGenres?
                        .Select(a => a.Genre.GenreName.ToString())
                        .ToList()

                });
            return upcomingMovies; 
        }


        public async Task<IEnumerable<MovieReadDto>> SearchMoviesByTitle(string keyword)
        {
            var movies = await _repo.GetAll();
            keyword = keyword.ToLower();

            var matchedMovies = movies
                .Where(m => m.Title.ToLower().Contains(keyword)) 
                .Select(m => new MovieReadDto
                {
                    MovieId = m.MovieId,
                    Title = m.Title,
                    ReleaseDate = m.ReleaseDate,
                    IsReleased = m.isReleased,
                    DirectorName = m.Director?.DirectorName,
                    GenreNames = m.MovieGenres?
                        .Select(a => a.Genre.GenreName.ToString())
                        .ToList(),
                    AverageRating = m.Reviews != null && m.Reviews.Any()
                        ? m.Reviews.Average(r => r.Stars)
                        : 0
                })
                .ToList();

            return matchedMovies;
        }


        public async Task<IEnumerable<MovieReadDto>> GetMovieByActor(string actor)
        {
            var movies= await _repo.GetAll();
            var moviesOfActor = movies
                .Where(a => a.MovieActors!
                .Any(b => b.Actor.ActorName.ToLower().Contains(actor.ToLower())))
                .Select(m => new MovieReadDto
                {
                    MovieId = m.MovieId,
                    Title = m.Title,
                    ReleaseDate = m.ReleaseDate,
                    IsReleased = m.isReleased,
                    DirectorName = m.Director?.DirectorName,
                    GenreNames = m.MovieGenres?
                        .Select(a => a.Genre.GenreName.ToString())
                        .ToList(),
                    AverageRating = m.Reviews != null && m.Reviews.Any()
                ? m.Reviews.Average(r => r.Stars)
                : 0
                });
            return moviesOfActor;
 
        }

        public async Task<IEnumerable<MovieReadDto>> GetLongestShortestMovie(string param)
        {
            string query = param.ToLower();
            var movies = await _repo.GetAll();
            IEnumerable<Movie> filteredMovies;

            if (query == "longest")
            {
                filteredMovies = movies.OrderByDescending(a => a.Duration).Take(1);
            }
            else if (query == "shortest")
            {
                filteredMovies = movies.OrderBy(a => a.Duration).Take(1);
            }
            else
            {
                return Enumerable.Empty<MovieReadDto>(); 
            }

            return filteredMovies.Select(a => new MovieReadDto
            {
                MovieId = a.MovieId,
                Title = a.Title,
                ReleaseDate = a.ReleaseDate,
                IsReleased = a.isReleased,
                DirectorName = a.Director?.DirectorName,
                GenreNames = a.MovieGenres?.Select(a => a.Genre.GenreName.ToString()).ToList(),
                AverageRating = a.Reviews != null && a.Reviews.Any()
                    ? a.Reviews.Average(r => r.Stars)
                    : 0
            }).ToList();
        }






    }
}
