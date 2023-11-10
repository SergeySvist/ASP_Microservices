using System.Runtime.Serialization.Json;
using System.Text;
using RabbitMQ.Client;

string exchangeName = "test.topic.exchange";

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using (IConnection connection = factory.CreateConnection())
{
    using (IModel channel = connection.CreateModel())
    {
        channel.ExchangeDeclare(exchangeName, ExchangeType.Topic, true);

        long messageNumber = 1;

        while (messageNumber < 20)
        {
            string itemAddedMessage = $"Item #{messageNumber}. Added";
            string itemUpdatedMessage = $"Item #{messageNumber}. Updated";
            string itemDeletedMessage = $"Item #{messageNumber}. Deleted";

            byte[] itemAddedBinaryBody = Encoding.Unicode.GetBytes(itemAddedMessage);
            byte[] itemUpdatedBinaryBody = Encoding.Unicode.GetBytes(itemUpdatedMessage);
            byte[] itemDeletedBinaryBody = Encoding.Unicode.GetBytes(itemDeletedMessage);

            channel.BasicPublish(
                exchange: exchangeName,
                routingKey: "Item.Added",
                mandatory: false,
                basicProperties: null,
                body: itemAddedBinaryBody
                );

            channel.BasicPublish(
                exchange: exchangeName,
                routingKey: "Item.Updated",
                mandatory: false,
                basicProperties: null,
                body: itemUpdatedBinaryBody
                );

            channel.BasicPublish(
                exchange: exchangeName,
                routingKey: $"Item.Message.To.Queue.Number.{messageNumber}.Deleted",
                mandatory: false,
                basicProperties: null,
                body: itemDeletedBinaryBody
                );

            messageNumber++;
            Thread.Sleep(1000);
        }

    }
}