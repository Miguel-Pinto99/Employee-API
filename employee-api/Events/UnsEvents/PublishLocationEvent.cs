using MediatR;
using employee_api.Models;

namespace employee_api.Events.UnsEvents
{
    public class PublishLocationEvent : INotification
    {
        public UsersEachLocation UsersEachLocation { get; set; }
        public string OfficeLocation { get; set; }
        public PublishLocationEvent(UsersEachLocation usersEachLocation, string officeLocation)
        {
            UsersEachLocation = usersEachLocation;
            OfficeLocation = officeLocation;
        }
    }
}
