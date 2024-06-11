namespace CMS.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool? Enabled { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<PlaylistContentItem> PlaylistContentItems { get; set; }
        public ICollection<PlaylistLabel> PlaylistLabels { get; set; }
        public Schedule Schedule { get; set; }
    }
}