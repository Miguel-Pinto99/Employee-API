using MediatR;
using employee_api.Models;

namespace employee_api.Events.UnsLogicEvents
{
    public class EditApplicationUserLogicEvent : INotification
    {
        public ApplicationUser NewApplicationUser { get; set; }
        public ApplicationUser OldApplicationUser { get; set; }
        public EditApplicationUserLogicEvent (ApplicationUser newApplicationUser,ApplicationUser oldApplicationUser)
        {
            NewApplicationUser = newApplicationUser;
            OldApplicationUser = oldApplicationUser;
        }
    }
}
