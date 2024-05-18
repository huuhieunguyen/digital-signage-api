namespace CMS.Models
{
    public class ContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? FilePath { get; set; }
        public ResourceType? ResourceType { get; set; }
        public int? Duration { get; set; } = 10;
        public string? Dimensions { get; set; }
        // public DateTime CreatedAt { get; set; }
        // public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        // public ICollection<Playlist> Playlists { get; set; }
        public ICollection<PlaylistContentItem> PlaylistContentItems { get; set; }
    }
    public enum ResourceType
    {
        Image,
        Video,
        Text
    }
}