using MediatR;
using employee_api.Models;

namespace employee_api.Events.UnsLogicEvents
{
    public class EditWorkPatternLogicEvent : INotification
    {
        public WorkPattern WorkPattern { get; set; }
        public EditWorkPatternLogicEvent (WorkPattern workPattern)
        {
            WorkPattern = workPattern;
        }
    }
}
