using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.Data;
using MovieManagementSystem.Models;

namespace MovieManagementSystem.Repository
{
    public class ActorRepository : ICRUD<Actor>
    {
        private readonly ManagementContext _context;
        public ActorRepository(ManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Actor>> GetAll()
        {
            return await _context.Actors
                .Include(a => a.MovieActors!).ThenInclude(b => b.Movie)
                .ToListAsync();
        }

        public async Task<Actor> GetById(int id)
        {
            return await _context.Actors.FirstOrDefaultAsync(a => a.ActorId == id) ?? throw new Exception("Post not found"); ;
        }

        public async Task<Actor> Add(Actor actor)
        {
            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();
            return actor;
        }

        public async Task<bool> Update(Actor actor)
        {
            var existingActor = await _context.Actors.FindAsync(actor.ActorId);
            if (existingActor == null)
                return false;

            _context.Entry(existingActor).CurrentValues.SetValues(actor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteById(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
                return false;

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
