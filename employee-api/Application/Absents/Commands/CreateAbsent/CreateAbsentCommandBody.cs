namespace employee_api.Application.Absent.Commands.CreateAbsent
{
    public class CreateAbsentCommandBody
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}