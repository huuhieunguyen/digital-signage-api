namespace CMS.DTOs.ScheduleDtos
{
    public class ScheduleResponseDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? DaysOfWeek { get; set; }
        public int PlaylistId { get; set; }
        public int PlayerId { get; set; }
    }
}
