namespace MovieManagementSystem.DTOs.Review
{
    public class ReviewCreateDto
    {
        public int MovieId { get; set; }
        public string ReviewerName { get; set; } = null!;
        public string? Comment { get; set; }
        public byte Stars { get; set; }
    }

    public class ReviewUpdateDto
    {
        public int ReviewId { get; set; }
        public string? Comment { get; set; }
        public byte Stars { get; set; }
    }

    public class ReviewReadDto
    {
        public int ReviewId { get; set; }
        public string ReviewerName { get; set; } = null!;
        public string? Comment { get; set; }
        public byte Stars { get; set; }
        public string MovieTitle { get; set; } = null!;
    }

    public class TopReviewerDto
    {
        public string ReviewerName { get; set; } = null!;
        public int ReviewCount { get; set; }
    }

}
