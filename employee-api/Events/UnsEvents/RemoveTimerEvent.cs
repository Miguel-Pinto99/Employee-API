using MediatR;
using employee_api.Models;

namespace employee_api.Events.UnsEvents
{
    public class RemoveTimerEvent : INotification
    {
        public ApplicationUser ApplicationUser { get; set; }
        public RemoveTimerEvent(ApplicationUser applicationUser)
        {
            ApplicationUser = applicationUser;
        }
    }
}
