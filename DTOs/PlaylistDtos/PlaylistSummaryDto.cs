using CMS.DTOs.ContentItemDtos;

namespace CMS.DTOs.PlaylistDtos
{
    public class PlaylistSummaryDto
    {
        public string Title { get; set; }
        public List<ContentItemSummaryDto> ContentItems { get; set; }
    }
}