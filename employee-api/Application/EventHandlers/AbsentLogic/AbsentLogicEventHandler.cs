using MediatR;
using employee_api.Application.Absent.Queries.GetAllAbsent;
using employee_api.Application.ApplicationUsers.Queries.GetApplicationUser;
using employee_api.Events.AbsentLogicEvents;
using employee_api.Events.UnsEvents;
using employee_api.Infrastructure;
using employee_api.Timers;

namespace employee_api.Application.EventHandlers.AbsentLogic
{
    public class AbsentLogicEventHandler : INotificationHandler<AbsentLogicEvent>
    {
        private readonly IUnsService _unsService;
        private readonly IMediator _mediator;
        private readonly ITimerService _timerService;

        public AbsentLogicEventHandler(IUnsService unsService
            , IMediator mediator
            , ITimerService timerService)
        {
            _unsService = unsService;
            _mediator = mediator;
            _timerService = timerService;
        }

        public async Task Handle(AbsentLogicEvent notification, CancellationToken cancellationToken)
        {
            if (notification is null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            Models.Absent absent = notification.Absent;

            var commandGetApplicationUser = new GetApplicationUserCommand(absent.UserId);
            var gotApplicationUser = await _mediator.Send(commandGetApplicationUser, cancellationToken);

            var commandGetAllAbsent = new GetAllAbsentCommand();
            var gotAllAbsentUsers = await _mediator.Send(commandGetAllAbsent, cancellationToken);

            await _unsService.CheckTodayAbsentsAsync(gotAllAbsentUsers.ListAbsent, cancellationToken);
            await _timerService.RemoveAbsentsAsync(gotAllAbsentUsers.ListAbsent, cancellationToken);

            var eventPublishWorkPattern = new PublishWorkPatternEvent(gotApplicationUser.ApplicationUser);
            await _mediator.Publish(eventPublishWorkPattern, cancellationToken);

            await _timerService.ReinitializeTimersAsync(gotApplicationUser.ApplicationUser, cancellationToken);

        }
    }
}