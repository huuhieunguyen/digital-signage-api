using CMS.DTOs.ScheduleDtos;

namespace CMS.DTOs.PlaylistDtos
{
    public class PlaylistCreateRequestDto
    {
        public string Title { get; set; }
        public List<int> ContentItemIds { get; set; }
        public List<int> LabelIds { get; set; }
        public ScheduleCreateRequestDto Schedule { get; set; }
    }
}
