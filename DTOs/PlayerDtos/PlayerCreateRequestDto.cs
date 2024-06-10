namespace CMS.DTOs.PlayerDtos
{
    public class PlayerCreateRequestDto
    {
        public string Name { get; set; }
        public string? Location { get; set; }
        public string? IPAddress { get; set; }
        public string? VirtualUrl { get; set; }
        public string? Resolution { get; set; }
        public string? Orientation { get; set; }
        public List<int> LabelIds { get; set; } // IDs of associated labels
    }
}