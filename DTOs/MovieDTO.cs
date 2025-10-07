using MovieManagementSystem.Models;

namespace MovieManagementSystem.DTOs.Movie
{

    public class MovieCreateDto
    {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public TimeSpan? Duration { get; set; }
        public bool IsReleased { get; set; }
        public int? DirectorId { get; set; }
        public List<int>? GenreIds { get; set; }
    }

    public class MovieUpdateDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public TimeSpan? Duration { get; set; }
        public bool IsReleased { get; set; }
        public int? DirectorId { get; set; }
        public List<int>? GenreIds { get; set; }
    }

    public class MovieReadDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsReleased { get; set; }
        public string? DirectorName { get; set; }
        public List<string>? GenreNames { get; set; }
        public double AverageRating { get; set; }
    }

    public class MovieReleaseDTO
    {
        public string Title { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string? DirectorName { get; set; }
        public List<string>? GenreNames { get; set; }
    }
}