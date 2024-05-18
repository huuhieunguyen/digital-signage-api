namespace CMS.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }
        // public StatusType? Status { get; set; }
        public DateTime? LastActiveDateTime { get; set; }
        public string? IPAddress { get; set; }
        public string? VirtualUrl { get; set; }
        public string? Resolution { get; set; }
        public string? Orientation { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        // public ICollection<Playlist> Playlists { get; set; }
        // public ICollection<Label> Labels { get; set; }
        public ICollection<PlayerPlaylist> PlayerPlaylists { get; set; }
        public ICollection<PlayerLabel> PlayerLabels { get; set; }
    }
    // public enum StatusType
    // {
    //     Online,
    //     Offline,
    //     OutOfSync,
    //     Inactive
    // }
}
