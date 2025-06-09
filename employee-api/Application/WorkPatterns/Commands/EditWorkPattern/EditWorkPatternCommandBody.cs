using MediatR;
using employee_api.Models;

namespace employee_api.Application.ApplicationUsers.Queries.EditWorkPatterm
{
    public class EditWorkPatternCommandBody
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<WorkPatternPart>? Parts { get; set; }
    }
}