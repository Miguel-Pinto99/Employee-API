using MediatR;
using employee_api.Events.UnsEvents;
using employee_api.Models;
using employee_api.Timers;

namespace employee_api.Application.EventHandlers.UnsEventHandlers
{
    public class StopTimerEventHandler : INotificationHandler<StopTimerEvent>
    {
        private readonly ITimerService _timerService;
        public StopTimerEventHandler(ITimerService timerService)
        {
            _timerService = timerService;
        }
        public async Task Handle(StopTimerEvent notification, CancellationToken cancellationToken)
        {
            if (notification is null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            Models.ApplicationUser applicationUser = notification.ApplicationUser;
            await _timerService.ReinitializeTimersAsync(applicationUser, cancellationToken);
        }
    }
}
