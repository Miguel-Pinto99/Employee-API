namespace employee_api.Infrastructure
{
    public interface IMqttService
    {
        Task PublishOnTopicAsync(string payLoad, string topic, CancellationToken cancellationToken);
    }
}