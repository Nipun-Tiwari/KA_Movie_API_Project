using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManagementSystem.Models
{
    public enum Gender
    {
        Male,
        Female,
        NonBinary,
        Other,
        NotDisclosed
    }
    
    public class Actor
    {
        [Key]
        public int ActorId { get; set; }
        [Required]
        [MaxLength(100)]
        public string ActorName { get; set; }
        public DateTime BirthDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? NetWorth { get; set; }
        public Gender? Gender { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
