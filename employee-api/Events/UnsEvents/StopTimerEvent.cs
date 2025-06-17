using MediatR;
using employee_api.Models;

namespace employee_api.Events.UnsEvents
{
    public class StopTimerEvent : INotification
    {
        public ApplicationUser ApplicationUser { get; set; }
        public StopTimerEvent(ApplicationUser applicationUser)
        {
            ApplicationUser = applicationUser;
        }
    }
}
