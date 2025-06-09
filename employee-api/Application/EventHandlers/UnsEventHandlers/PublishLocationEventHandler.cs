using MediatR;
using employee_api.Events.UnsEvents;
using employee_api.Infrastructure;
using employee_api.Models;
using employee_api.Persistance;
using System.Diagnostics;
using System.Net.NetworkInformation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace employee_api.Application.Uns.EventHandlers
{
    public class PublishLocationEventHandler : INotificationHandler<PublishLocationEvent>
    {

        private readonly IUnsService _unsService;

        public PublishLocationEventHandler(IUnsService unsService)
        {
            _unsService = unsService;
        }

        public async Task Handle(PublishLocationEvent notification, CancellationToken cancellationToken)
        {
            if (notification is null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            UsersEachLocation usersEachLocation = notification.UsersEachLocation;
            int officeLocation = notification.OfficeLocation;


            await _unsService.PublishLocationAsync(usersEachLocation, officeLocation, cancellationToken);
        }


    }
}
