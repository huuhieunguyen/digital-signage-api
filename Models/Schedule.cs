namespace CMS.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? DaysOfWeek { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Foreign key
        public int PlaylistId { get; set; }

        // Navigation property
        public Playlist Playlist { get; set; }
    }
}