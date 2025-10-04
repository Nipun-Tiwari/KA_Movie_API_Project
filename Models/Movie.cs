using System.ComponentModel.DataAnnotations;

namespace MovieManagementSystem.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        public TimeSpan? Duration {  get; set; }
        public bool isReleased { get; set; }

        public int? DirectorId { get; set; }
        public Director? Director { get; set; }
   
        public ICollection<MovieGenre>? MovieGenres { get; set; }
        public ICollection<MovieActor>? MovieActors { get; set; }
        public ICollection<Review>? Reviews { get; set; }



    }
}
