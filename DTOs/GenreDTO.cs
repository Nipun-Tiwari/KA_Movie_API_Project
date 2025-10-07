using MovieManagementSystem.Models;

namespace MovieManagementSystem.DTOs.Genre
{
    public class GenreCreateDto
    {
        public GenreType GenreName { get; set; }
    }

    public class GenreUpdateDto
    {
        public int GenreId { get; set; }
        public GenreType GenreName { get; set; }
    }

    public class GenreReadDto
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; } = null!;
        public List<string>? Movies { get; set; }
    }
}
