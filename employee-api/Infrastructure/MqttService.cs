using MQTTnet.Client;
using MQTTnet;

namespace employee_api.Infrastructure
{
    public class MqttService : IMqttService
    {
        public async Task PublishOnTopicAsync(string payLoad, string topic, CancellationToken cancellationToken)

        {
            var mqttFactory = new MqttFactory();
            string ip = "emqx";

            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer(ip)
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
