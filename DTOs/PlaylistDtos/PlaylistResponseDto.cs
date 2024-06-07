using CMS.DTOs.ContentItemDtos;
using CMS.DTOs.LabelDtos;
using CMS.DTOs.ScheduleDtos;

namespace CMS.DTOs.PlaylistDtos
{
    public class PlaylistResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<ContentItemResponseDto> ContentItems { get; set; }
        public List<LabelResponseDto> Labels { get; set; }
        public ScheduleResponseDto Schedule { get; set; }
    }
}
