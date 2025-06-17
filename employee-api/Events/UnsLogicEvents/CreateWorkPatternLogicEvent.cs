using MediatR;
using employee_api.Models;

namespace employee_api.Events.UnsLogicEvents
{
    public class CreateWorkPatternLogicEvent : INotification
    {
        public WorkPattern WorkPattern { get; set; }
        public CreateWorkPatternLogicEvent (WorkPattern workPattern)
        {
            WorkPattern = workPattern;
        }
    }
}
