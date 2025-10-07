namespace MovieManagementSystem.DTOs.Director
{
    public class DirectorCreateDto
    {
        public string DirectorName { get; set; } = null!;
        public int? YearsOfExperience { get; set; }
        public bool? IsActive { get; set; }
        public string? Email { get; set; }
    }

    public class DirectorUpdateDto
    {
        public int DirectorId { get; set; }
        public string DirectorName { get; set; } = null!;
        public int? YearsOfExperience { get; set; }
        public bool? IsActive { get; set; }
        public string? Email { get; set; }
    }

    public class DirectorReadDto
    {
        public int DirectorId { get; set; }
        public string DirectorName { get; set; } = null!;
        public int? YearsOfExperience { get; set; }
        public bool? IsActive { get; set; }
        public string? Email { get; set; }
        public List<string>? Movies { get; set; }
    }
}
