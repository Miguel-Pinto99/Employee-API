using MediatR;
using employee_api.Application.ApplicationUsers.Queries.GetApplicationUser;
using employee_api.Events.UnsEvents;
using employee_api.Events.UnsLogicEvents;
using employee_api.Models;
using employee_api.Timers;

namespace employee_api.Application.Uns.UnsLogicEventHandlers
{
    public class CreateWorkPatternUnsLogicEventHandler : INotificationHandler<CreateWorkPatternLogicEvent>
    {
        private readonly IMediator _mediator;
        public CreateWorkPatternUnsLogicEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Handle(CreateWorkPatternLogicEvent notification, CancellationToken cancellationToken)
        {
            if (notification is null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            WorkPattern createdWorkPattern = notification.WorkPattern;

            var commandGetApplicationUser = new GetApplicationUserCommand(createdWorkPattern.UserId);
            var gotApplicationUser = await _mediator.Send(commandGetApplicationUser, cancellationToken);

            var eventPublishWorkPattern = new PublishWorkPatternEvent(gotApplicationUser.ApplicationUser);
            await _mediator.Publish(eventPublishWorkPattern, cancellationToken);

            var eventStopTimer = new StopTimerEvent(gotApplicationUser.ApplicationUser);
            await _mediator.Publish(eventStopTimer, cancellationToken);
        }      

    }
}
