namespace CMS.Models
{
    public class Label
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Player> Players { get; set; }
        public ICollection<Playlist> Playlists { get; set; }
    }
}