using MediatR;
using employee_api.Models;

namespace employee_api.Events.UnsLogicEvents
{
    public class DeleteWorkPatternLogicEvent : INotification
    {
        public WorkPattern WorkPattern { get; set; }
        public DeleteWorkPatternLogicEvent (WorkPattern workPattern)
        {
            WorkPattern = workPattern;
        }
    }
}
