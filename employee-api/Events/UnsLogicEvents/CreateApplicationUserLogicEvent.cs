using MediatR;
using employee_api.Models;

namespace employee_api.Events.UnsLogicEvents
{
    public class CreateApplicationUserLogicEvent : INotification
    {
        public ApplicationUser ApplicationUser { get; set; }
        public CreateApplicationUserLogicEvent (ApplicationUser applicationUser)
        {
            ApplicationUser = applicationUser;
        }
    }
}
