namespace CMS.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? DaysOfWeek { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key
        public int PlayerId { get; set; }
        public int PlaylistId { get; set; }

        // Navigation property
        public Playlist Playlist { get; set; }
        public Player Player { get; set; }
    }
}