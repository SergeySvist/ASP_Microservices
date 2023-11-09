using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryMS.Events;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Threading.Channels;
using System.Text.Json;

namespace InvoiceMS.Infrastructure.MessageBroker
{
    public class InventoryMsEventsConsumer: IHostedService
    {
        private Timer? _timer = null;

        string messageQueueName = "inventory_ms.events.for.invoice_ms";

        private readonly ConnectionFactory rabbitMqConnectionFactory = new ConnectionFactory { HostName = "localhost" };
        private readonly IConnection rabbitMqConnection;
        private readonly IModel rabbitMqChannel;
        private readonly EventingBasicConsumer channelConsumer;
        private readonly IInventoryMsEventsProcessor _inventoryMsEventsProcessor;

        public InventoryMsEventsConsumer(IInventoryMsEventsProcessor inventoryMsEventsProcessor)
        {
            _inventoryMsEventsProcessor = inventoryMsEventsProcessor;

            try
            {
                rabbitMqConnection = rabbitMqConnectionFactory.CreateConnection();
                rabbitMqChannel = rabbitMqConnection.CreateModel();
                rabbitMqChannel.QueueDeclare(
                    queue: messageQueueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );

                rabbitMqChannel.QueueBind(messageQueueName, InventoryMS.Events.Constants.ExchangeName, "");

                channelConsumer = new EventingBasicConsumer(rabbitMqChannel);

                SetupEventsConsumer(channelConsumer);

            }
            catch (Exception)
            {
                throw;
            }        
        }

        private void SetupEventsConsumer(EventingBasicConsumer channelConsumer)
        {
            channelConsumer.Received += (sender, args) =>
            {
                byte[] messageBody = args.Body.ToArray();
                string jsonMessageBody = Encoding.UTF8.GetString(messageBody);

                try
                {
                    InventoryMsEvent? eventReceived = JsonSerializer.Deserialize<InventoryMsEvent>(jsonMessageBody);

                    if (eventReceived != null)
                    {
                        _inventoryMsEventsProcessor.ProcessEvent(eventReceived);

                    }
                }
                catch (Exception)
                {
                    Debug.WriteLine("Error processing incoming event");
                    throw;
                }            
            };
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(ConsumeEventBusMessages, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        void ConsumeEventBusMessages(object state)
        {
            rabbitMqChannel.BasicConsume(messageQueueName, true, channelConsumer);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
