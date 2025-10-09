using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.Models;

namespace MovieManagementSystem.Data
{
    public class ManagementContext: DbContext
    {
        //DbSet
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }  
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieGenre> MoviesGenres { get; set; }


        //constructor
        public ManagementContext(DbContextOptions<ManagementContext> options) : base(options) { }

        //methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // MovieActor junction (many-to-many)
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId)
                .OnDelete(DeleteBehavior.Cascade);

            // MovieGenre junction (many-to-many)
            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenreId)
                .OnDelete(DeleteBehavior.Cascade);

            // Movie - Director (one-to-many)
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Director)
                .WithMany(d => d.Movies)
                .HasForeignKey(m => m.DirectorId)
                .OnDelete(DeleteBehavior.SetNull);

            // Movie - Review (one-to-many)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Movie)
                .WithMany(m => m.Reviews)
                .HasForeignKey(r => r.MovieId)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<Actor>()
                .Property(a => a.Gender)
                .HasConversion<string>();

            modelBuilder.Entity<Genre>()
                .Property(g => g.GenreName)
                .HasConversion<string>();
        }
    }
}
