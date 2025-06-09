using MediatR;
using employee_api.Models;

namespace employee_api.Application.WorkPatterns.Commands.CreateWorkPattern
{
    public class CreateWorkPatternCommandBody
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<WorkPatternPart>? Parts { get; set; }
    }
}