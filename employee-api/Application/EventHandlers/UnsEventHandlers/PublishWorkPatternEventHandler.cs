using MediatR;
using employee_api.Events.UnsEvents;
using employee_api.Infrastructure;
using employee_api.Models;

namespace employee_api.Application.Uns.EventHandlers
{
    public class PublishWorkPatternEventHandler : INotificationHandler<PublishWorkPatternEvent>
    {
            private readonly IUnsService _unsService;

            public PublishWorkPatternEventHandler(IUnsService unsService)
            {
                _unsService = unsService;
            }

            public async Task Handle(PublishWorkPatternEvent notification, CancellationToken cancellationToken)
            {
                if (notification is null)
                {
                    throw new ArgumentNullException(nameof(notification));
                }

                Models.ApplicationUser applicationUser = notification.ApplicationUser;
                await _unsService.CallListWorkPatternAsync(applicationUser, cancellationToken);
            }
    }
}
