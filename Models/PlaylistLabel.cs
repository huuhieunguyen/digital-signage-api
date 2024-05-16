namespace CMS.Models
{
    public class PlaylistLabel
    {
        public int PlaylistId { get; set; }
        public int LabelId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Playlist Playlist { get; set; }
        public Label Label { get; set; }
    }
}