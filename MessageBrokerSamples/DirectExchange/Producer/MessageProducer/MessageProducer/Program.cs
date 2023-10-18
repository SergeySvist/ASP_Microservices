using System.Text;
using RabbitMQ.Client;

string firstMessageQueueName = "first.message.queue";
string secondMessageQueueName = "second.message.queue";

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using (IConnection connection = factory.CreateConnection())
{
    using (IModel channel = connection.CreateModel())
    {
        channel.QueueDeclare(
            queue: firstMessageQueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
            );

        byte[] binaryMessageBody = Encoding.Unicode.GetBytes("Message #1. Hello buddy. What's up");

        channel.BasicPublish(
            exchange: string.Empty,
            routingKey: firstMessageQueueName,
            mandatory: false,
            basicProperties: null,
            body: binaryMessageBody
            );
    }
}
