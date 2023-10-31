using System.Text;
using RabbitMQ.Client;

string firstMessageQueueName = "first.message.queue";
string secondMessageQueueName = "second.message.queue";

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using (IConnection connection = factory.CreateConnection())
{
    using (IModel channel = connection.CreateModel())
    {
        #region messageToFirstQueue
        channel.QueueDeclare(
            queue: firstMessageQueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
            );

        long messageNumber = 1;

        while (messageNumber < 10000)
        {

            byte[] binaryMessageBody = Encoding.Unicode.GetBytes($"Message #{messageNumber++}. Hello buddy. What's up");

            channel.BasicPublish(
                exchange: string.Empty,
                routingKey: firstMessageQueueName,
                mandatory: false,
                basicProperties: null,
                body: binaryMessageBody
                );

        }

        #endregion

        //#region messageToSecondQueue
        //channel.QueueDeclare(
        //    queue: secondMessageQueueName,
        //    durable: true,
        //    exclusive: false,
        //    autoDelete: false,
        //    arguments: null
        //    );


        //binaryMessageBody = Encoding.Unicode.GetBytes("Message #1 to Queue #2. Hello buddy. What's up");

        //channel.BasicPublish(
        //    exchange: string.Empty,
        //    routingKey: secondMessageQueueName,
        //    mandatory: false,
        //    basicProperties: null,
        //    body: binaryMessageBody
        //    );
        //#endregion
    }
}
