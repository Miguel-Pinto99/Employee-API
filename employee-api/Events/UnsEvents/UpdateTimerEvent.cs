using MediatR;
using employee_api.Models;

namespace employee_api.Events.UnsEvents
{
    public class UpdateTimerEvent : INotification
    {
        public ApplicationUser ApplicationUser { get; set; }
        public UpdateTimerEvent(ApplicationUser applicationUser)
        {
            ApplicationUser = applicationUser;
        }
    }
}
