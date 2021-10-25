using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using iemobile.ViewModels;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using Xamarin.Forms;

namespace iemobile.Services
{
    public class MqttService
    {
        public static IMqttClientOptions options;
        public static IMqttClient mqttClient;
        public MqttService()
        {
            options = new MqttClientOptionsBuilder()
                 .WithClientId($"Mobile+{Guid.NewGuid()}")
                 .WithTcpServer(Settings.MqttEndpoint, Settings.MqttPort)
                 .WithCleanSession()
                 .Build();

            var factory = new MqttFactory();
            mqttClient = factory.CreateMqttClient();
            mqttClient.UseDisconnectedHandler(async e => await DisconnectedHandler(e));


            Connect();
        }


        async Task Connect()
        {
            try
            {
                await mqttClient.ConnectAsync(options, CancellationToken.None);
                if (mqttClient.IsConnected)
                {
                    Debug.WriteLine($"Mqtt Connected @{DateTime.Now}");
                    await mqttClient.SubscribeAsync("#");
                    mqttClient.UseApplicationMessageReceivedHandler(e =>
                    {
                        var payload = "";

                        if (e.ApplicationMessage.Payload != null)
                        {
                            payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                        }
                        MessagingCenter.Send(this, "message", (e.ApplicationMessage.Topic ?? "") + "|" + payload);
                        Debug.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                        Debug.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
                        Debug.WriteLine($"+ Payload = {payload}\n");
                    });
                }
            }
            catch
            {
                Debug.WriteLine("Trying to reconnect");
            }
        }

        private async Task DisconnectedHandler(MqttClientDisconnectedEventArgs arg)
        {
            Debug.WriteLine("### DISCONNECTED FROM SERVER ###");
            await Task.Delay(TimeSpan.FromSeconds(5));

            try
            {
                await mqttClient.ConnectAsync(options, CancellationToken.None);
            }
            catch
            {
                Debug.WriteLine("### RECONNECTING FAILED ###");
            }
        }


    }
}
