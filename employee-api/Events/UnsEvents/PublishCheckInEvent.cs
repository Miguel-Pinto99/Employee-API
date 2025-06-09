using MediatR;
using employee_api.Models;

namespace employee_api.Events.UnsEvents
{
    public class PublishCheckInEvent : INotification
    {
        public ApplicationUser ApplicationUser { get; set; }
        public PublishCheckInEvent(ApplicationUser applicationUser)
        {
            ApplicationUser = applicationUser;
        }

    }
}
