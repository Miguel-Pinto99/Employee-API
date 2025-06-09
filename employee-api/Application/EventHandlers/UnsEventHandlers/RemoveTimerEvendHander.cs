using MediatR;
using employee_api.Events.UnsEvents;
using employee_api.Models;
using employee_api.Timers;

namespace employee_api.Application.Uns.UnsLogicEventHandlers
{
    public class RemoveTimerEventHandler : INotificationHandler<RemoveTimerEvent>
    {
        private readonly ITimerService _timerService;
        public RemoveTimerEventHandler(ITimerService timerService)
        {
            _timerService = timerService;
        }
        public async Task Handle(RemoveTimerEvent notification, CancellationToken cancellationToken)
        {
            if (notification is null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            Models.ApplicationUser applicationUser = notification.ApplicationUser;
            await _timerService.RemoveTimersAsync(applicationUser,cancellationToken);
        }




    }
}