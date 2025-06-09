using employee_api.Models;

namespace employee_api.Application.Absent.Queries.GetAllAbsent
{
    public class GetAllAbsentResponse
    {
        public List<Models.Absent> ListAbsent { get; set; }
    }
}
