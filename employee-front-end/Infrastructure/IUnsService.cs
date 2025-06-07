using employee_front_end.Model;

namespace employee_front_end.Infrastructure
{
    public interface IUnsService
    {
        public Task<List<ApplicationUser>> SubscribeBrokerAsync(CancellationToken cancellationToken);
    }
}
