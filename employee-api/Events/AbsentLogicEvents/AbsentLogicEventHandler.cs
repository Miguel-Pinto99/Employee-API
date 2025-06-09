using MediatR;
using employee_api.Events.AbsentLogicEvents;
using employee_api.Events.UnsEvents;
using employee_api.Infrastructure;
using employee_api.Models;

namespace employee_api.Events.AbsentLogicEvents
{
    public class AbsentLogicEvent : INotification
    {
        public Absent Absent { get; set; }
        public AbsentLogicEvent(Absent absent)
        {
            Absent = absent;
        }
    }
}