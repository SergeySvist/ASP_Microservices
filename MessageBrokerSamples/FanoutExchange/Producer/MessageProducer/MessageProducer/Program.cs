using System.Runtime.Serialization.Json;
using System.Text;
using RabbitMQ.Client;

string exchangeName = "castom.fanout.exchange";

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using (IConnection connection = factory.CreateConnection())
{
    using (IModel channel = connection.CreateModel())
    {
        channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, true);

        long messageNumber = 1;

        while (messageNumber < 100)
        {
            Console.WriteLine($"Message #{messageNumber} sended to queues");
            byte[] binaryMessageBody = Encoding.Unicode.GetBytes($"Message #{messageNumber++}. For any queue of amq.fanout");

            channel.BasicPublish(
                exchange: exchangeName,
                routingKey: "",
                mandatory: false,
                basicProperties: null,
                body: binaryMessageBody
                );

            Thread.Sleep( 1000 );
        }

    }
}