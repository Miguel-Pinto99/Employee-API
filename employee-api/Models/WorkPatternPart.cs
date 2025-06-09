using System.Text.Json.Serialization;

namespace employee_api.Models
{
    public class WorkPatternPart
    {
        public Guid Id { get; set; }

        public DayOfWeek Day { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}



