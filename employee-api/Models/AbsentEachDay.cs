namespace employee_api.Models
{
    public class AbsentEachDay
    {
        public int DayOfYear { get; set; }
        public List<int> ListIds { get; set; }

        public AbsentEachDay(int dayOfYear, List<int> listIds)
        {
            DayOfYear = dayOfYear;
            ListIds = listIds;
        }

    }
}
