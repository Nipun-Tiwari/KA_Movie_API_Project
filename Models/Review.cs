using System.ComponentModel.DataAnnotations;

namespace MovieManagementSystem.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }   

        [Required]
        public int MovieId { get; set; }   

        [Required]
        [MaxLength(100)]
        public string ReviewerName { get; set; }   

        [MaxLength(500)]
        public string? Comment { get; set; }  

        [Range(1, 5)]
        public byte Stars { get; set; }  

        public Movie Movie { get; set; } = null!;
    }
}
