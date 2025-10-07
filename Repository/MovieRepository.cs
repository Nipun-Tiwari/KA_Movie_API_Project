using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MovieManagementSystem.Data;
using MovieManagementSystem.Models;

namespace MovieManagementSystem.Repository
{
    public class MovieRepository: ICRUD<Movie>
    {
        private readonly ManagementContext _context;
        public MovieRepository(ManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _context.Movies
                .Include(m => m.Director!)
                .Include(p => p.MovieActors!).ThenInclude(q => q.Actor)
                .Include(r => r.MovieGenres!).ThenInclude(s => s.Genre).ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _context.Movies.FirstOrDefaultAsync(a=>a.MovieId==id) ?? throw new Exception("Post not found"); ;
        }

        public async Task<Movie> Add(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<bool> Update(Movie movie)
        {
            var existingMovie = await _context.Movies.FindAsync(movie.MovieId);
            if (existingMovie == null)
                return false;

            _context.Entry(existingMovie).CurrentValues.SetValues(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteById(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return false;

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
