using MediatR;
using employee_api.Events.UnsEvents;
using employee_api.Infrastructure;
using employee_api.Models;
using System.Threading;

namespace employee_api.Application.EventHandlers
{
    public class DeleteTopicApplicationUserEventHandler: INotificationHandler<DeleteTopicApplicationUserEvent>
    {
        private readonly IUnsService _unsService;
        public DeleteTopicApplicationUserEventHandler(IUnsService unsService)
        {
            _unsService = unsService;
        }
        public async Task Handle(DeleteTopicApplicationUserEvent notification, CancellationToken cancellationToken)
        {
            if (notification is null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            Models.ApplicationUser applicationUser = notification.ApplicationUser;
            await _unsService.DeleteTopicApplicationUserAsync(applicationUser, cancellationToken);
        }
    }
}
