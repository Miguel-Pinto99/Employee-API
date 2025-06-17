namespace employee_api.Models
{
    public class AbsentPayLoad
    {        
        public TimeSpan From { get; set; }
        public TimeSpan Till { get; set; }
        public string Reason { get; set; }
    }
}
