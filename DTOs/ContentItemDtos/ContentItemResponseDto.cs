using CMS.Models;

namespace CMS.DTOs.ContentItemDtos
{
    public class ContentItemResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public ResourceType ResourceType { get; set; }
        public int Duration { get; set; }
        public string? Description { get; set; }
        // public string? ThumbnailUrl { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string? Dimensions { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}