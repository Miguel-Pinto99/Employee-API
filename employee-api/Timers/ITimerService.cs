using employee_api.Models;

namespace employee_api.Timers
{
    public interface ITimerService
    {
        public Task SetInitialAbsentAsync(List<Absent> listAllAbsentss, CancellationToken cancellationToken);
        public Task RemoveAbsentsAsync(List<Absent> listAllAbsentss, CancellationToken cancellationToken);
        public Task SetAbsentsAsync(List<Absent> listAllAbsents, CancellationToken cancellationToken);
        public Task CalculateDelayAsync(ApplicationUser applicationUser, CancellationToken cancellationToken);
        public Task ReinitializeTimersAsync(ApplicationUser applicationUser, CancellationToken cancellationToken);
        public Task RemoveTimersAsync(ApplicationUser applicationUser, CancellationToken cancellationToken);
        public Task SetInitialTimerAsync(List<ApplicationUser> applicationUsers, CancellationToken cancellationToken);
    }
}
