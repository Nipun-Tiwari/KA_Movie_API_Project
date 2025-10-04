using System.ComponentModel.DataAnnotations;

namespace MovieManagementSystem.Models
{
    public class Director
    {
        [Key]
        public int DirectorId {  get; set; }
        [Required]
        [MaxLength(150)]
        public string DirectorName { get; set; }
        public int? YearsOfExperience { get; set; }
        public bool? IsActive { get; set; }
        [EmailAddress]
        [MaxLength(200)]
        public string? Email { get; set; }  

        public ICollection<Movie>? Movies { get; set; }



    }
}
