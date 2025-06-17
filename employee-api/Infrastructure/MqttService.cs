using MQTTnet.Client;
using MQTTnet;

namespace employee_api.Infrastructure
{
    public class MqttService : IMqttService
    {
        private readonly string _mqttHost;
        private readonly int _mqttPort;

        public MqttService(string mqttHost = "emqx", int mqttPort = 1883)
        {
            _mqttHost = mqttHost;
            _mqttPort = mqttPort;
        }

        public async Task PublishOnTopicAsync(string payLoad, string topic, CancellationToken cancellationToken)
        {
            var mqttFactory = new MqttFactory();

            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer(_mqttHost, _mqttPort)
                    .Build();

                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                var applicationMessage = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(payLoad)
                    .WithRetainFlag(true)
                    .Build();

                await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
            }
        }
    }
}
