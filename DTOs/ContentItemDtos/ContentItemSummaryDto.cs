using CMS.Models;

namespace CMS.DTOs.ContentItemDtos
{
    public class ContentItemSummaryDto
    {
        public string Title { get; set; }
        public string FilePath { get; set; }
        public ResourceType ResourceType { get; set; }
        public int? Duration { get; set; }
        public string Dimensions { get; set; }
    }
}