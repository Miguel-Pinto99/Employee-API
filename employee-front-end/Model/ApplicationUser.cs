namespace employee_front_end.Model
{
    public class ApplicationUser
    {
        public string? Id { get; set; }
        public PersonalInfo PersonalInfo { get; set; }       
        public bool Checked_In { get; set; }
        public bool ScheduleWorkNow { get; set; }
        public TodayShift? TodayShift { get; set; }
        public TodayAbsent? TodayAbsent { get; set; }
        public string RoleId { get; set; }
        
        public string ScheduleWorkToday { get; set; }

    }
}
