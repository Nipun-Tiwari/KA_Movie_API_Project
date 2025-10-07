using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.Data;
using MovieManagementSystem.Models;

namespace MovieManagementSystem.Repository
{
    public class GenreRepository: ICRUD<Genre>
    {
        private readonly ManagementContext _context;

        public GenreRepository(ManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _context.Genres
                .Include(g => g.MovieGenres!)
                .ThenInclude(mg => mg.Movie)
                .ToListAsync();
        }

        public async Task<Genre> GetById(int id)
        {
            return await _context.Genres
                .Include(g => g.MovieGenres!)
                .ThenInclude(mg => mg.Movie)
                .FirstOrDefaultAsync(g => g.GenreId == id)
                ?? throw new Exception("Genre not found");
        }

        public async Task<Genre> Add(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<bool> Update(Genre genre)
        {
            var existingGenre = await _context.Genres.FindAsync(genre.GenreId);
            if (existingGenre == null)
                return false;

            _context.Entry(existingGenre).CurrentValues.SetValues(genre);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteById(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
                return false;

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
