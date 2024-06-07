namespace CMS.DTOs.ScheduleDtos
{
    public class ScheduleCreateRequestDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? DaysOfWeek { get; set; }
    }
}
