using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CMS.Models
{

    public class ContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? FilePath { get; set; }
        // public string? ThumbnailUrl { get; set; }
        public int? Duration { get; set; } = 10;
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string? Dimensions { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Store ResourceType as a string
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ResourceType? ResourceType { get; set; }

        // Navigation properties
        public ICollection<PlaylistContentItem> PlaylistContentItems { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ResourceType
    {
        [EnumMember(Value = "Image")]
        Image,

        [EnumMember(Value = "Video")]
        Video
    }
}