namespace CMS.Models
{
    public class PlayerLabel
    {
        public int PlayerId { get; set; }
        public int LabelId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Player Player { get; set; }
        public Label Label { get; set; }
    }
}