using System;

namespace Models
{
    public class ContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}