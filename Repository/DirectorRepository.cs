using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.Data;
using MovieManagementSystem.Models;

namespace MovieManagementSystem.Repository
{
    public class DirectorRepository: ICRUD<Director>
    {
        private readonly ManagementContext _context;

        public DirectorRepository(ManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Director>> GetAll()
        {
            return await _context.Directors
                .Include(d => d.Movies!)
                .ToListAsync();
        }

        public async Task<Director> GetById(int id)
        {
            return await _context.Directors
                .Include(d => d.Movies!)
                .FirstOrDefaultAsync(d => d.DirectorId == id)
                ?? throw new Exception("Director not found");
        }

        public async Task<Director> Add(Director director)
        {
            await _context.Directors.AddAsync(director);
            await _context.SaveChangesAsync();
            return director;
        }

        public async Task<bool> Update(Director director)
        {
            var existingDirector = await _context.Directors.FindAsync(director.DirectorId);
            if (existingDirector == null)
                return false;

            _context.Entry(existingDirector).CurrentValues.SetValues(director);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteById(int id)
        {
            var director = await _context.Directors.FindAsync(id);
            if (director == null)
                return false;

            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
