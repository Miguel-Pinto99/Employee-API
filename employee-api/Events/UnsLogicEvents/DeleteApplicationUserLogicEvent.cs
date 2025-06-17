using MediatR;
using employee_api.Models;

namespace employee_api.Events.UnsLogicEvents
{
    public class DeleteApplicationUserLogicEvent : INotification
    {
        public ApplicationUser ApplicationUser { get; set; }
        public UsersEachLocation UsersEachLocation { get; set; }
        public DeleteApplicationUserLogicEvent (ApplicationUser applicationUser, UsersEachLocation usersEachLocation)
        {
            ApplicationUser = applicationUser;
            UsersEachLocation = usersEachLocation;
        }
    }
}
