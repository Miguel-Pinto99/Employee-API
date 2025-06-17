using MediatR;
using employee_api.Models;

namespace employee_api.Events.UnsEvents
{
    public class DeleteTopicApplicationUserEvent : INotification
    {
        public ApplicationUser ApplicationUser { get; set; }
        public DeleteTopicApplicationUserEvent(ApplicationUser applicationUser)
        {
            ApplicationUser = applicationUser;
        }
    }
}
