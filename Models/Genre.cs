using System.ComponentModel.DataAnnotations;

namespace MovieManagementSystem.Models
{
    public enum GenreType
    {
        Action,
        Comedy,
        Drama,
        Thriller,
        SciFi,
        Horror
    }
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        [Required]
        public GenreType GenreName { get; set; }

        public ICollection<MovieGenre>? MovieGenres { get; set; }
    }
}
    