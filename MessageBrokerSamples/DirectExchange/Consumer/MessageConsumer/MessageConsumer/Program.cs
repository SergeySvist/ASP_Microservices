using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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


        EventingBasicConsumer channelConsumer = new EventingBasicConsumer(channel);

        channelConsumer.Received += (sender, args) =>
        {
            byte[] messageBody = args.Body.ToArray();

            string received = Encoding.UTF8.GetString(messageBody);
            Console.WriteLine($"Message received: {received}" );
            Console.WriteLine($"Exchange: {args.Exchange}");
            Console.WriteLine($"RoutingKey: {args.RoutingKey}");

            Thread.Sleep(1000);

        };

        while (true)
        {
            channel.BasicConsume(firstMessageQueueName, autoAck: true, consumer: channelConsumer);
        }
    }
}

