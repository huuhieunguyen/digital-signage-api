namespace CMS.Models
{
    public class PlaylistContentItem
    {
        public int PlaylistId { get; set; }
        public int ContentItemId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Playlist Playlist { get; set; }
        public ContentItem ContentItem { get; set; }
    }
}