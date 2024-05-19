namespace CMS.Models
{
    public class Label
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<PlayerLabel> PlayerLabels { get; set; }
        public ICollection<PlaylistLabel> PlaylistLabels { get; set; }
    }
}