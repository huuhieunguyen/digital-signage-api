using CMS.DTOs.LabelDtos;
using CMS.DTOs.PlaylistDtos;

namespace CMS.DTOs.PlayerDtos
{
    public class PlayerResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }
        public DateTime? LastActiveDateTime { get; set; }
        public string? IPAddress { get; set; }
        public string? VirtualUrl { get; set; }
        public string? Resolution { get; set; }
        public string? Orientation { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<LabelResponseDto> Labels { get; set; }
        public List<PlaylistSummaryDto> Playlists { get; set; }
    }
}