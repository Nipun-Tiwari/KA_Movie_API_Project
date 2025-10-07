using MovieManagementSystem.Models;

namespace MovieManagementSystem.DTOs.Actor
{
    public class ActorCreateDto
    {
        public string ActorName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public decimal? NetWorth { get; set; }
        public Gender? Gender { get; set; }
    }

    public class ActorUpdateDto
    {
        public int ActorId { get; set; }
        public string ActorName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public decimal? NetWorth { get; set; }
        public Gender? Gender { get; set; }
    }

    public class ActorReadDto
    {
        public int ActorId { get; set; }
        public string ActorName { get; set; } = null!;
        public string? Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public List<string>? Movies { get; set; }
    }
}
