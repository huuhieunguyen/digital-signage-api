namespace CMS.DTOs.ContentItemDtos
{
    public class ContentItemCreateRequestDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public IFormFile File { get; set; }
    }
}