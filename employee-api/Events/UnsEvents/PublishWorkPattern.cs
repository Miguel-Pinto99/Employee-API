using MediatR;

using employee_api.Models;

namespace employee_api.Events.UnsEvents
{
    public class PublishWorkPatternEvent : INotification
    {
        public ApplicationUser ApplicationUser { get; set; }
        public PublishWorkPatternEvent(ApplicationUser applicationUser)
        {
            ApplicationUser = applicationUser;
        }
    }
}
