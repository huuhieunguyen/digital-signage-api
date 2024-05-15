using System;

namespace CMS.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Enable { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}