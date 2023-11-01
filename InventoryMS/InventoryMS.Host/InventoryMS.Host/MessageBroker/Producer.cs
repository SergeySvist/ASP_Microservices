using System.Text;
using System.Text.Json;
using InventoryMS.Events;
using RabbitMQ.Client;

namespace InventoryMS.Host.MessageBroker
{
    public class Producer : IMessageBusProducer
    {
        private ConnectionFactory rabbitConnectionFactory = new ConnectionFactory { HostName = "localhost" };

        public bool PublishEvent(InventoryMsEvent eventToPublish)
        {
            try
            {
                using (IConnection connection = rabbitConnectionFactory.CreateConnection())
                {

                    using (IModel channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(Events.Constants.ExchangeName, ExchangeType.Fanout, true);

                        string eventJson = JsonSerializer.Serialize(eventToPublish);
                        byte[] binaryEventBody = Encoding.UTF8.GetBytes(eventJson);

                        channel.BasicPublish(
                            exchange: Events.Constants.ExchangeName,
                            routingKey: "",
                            mandatory: false,
                            basicProperties: null,
                            body: binaryEventBody
                        );

                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}