using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace BusinessLayer.Services
{
    public class RabbitMQConsumer
    {
        private readonly ConnectionFactory _factory;
        private readonly string _queueName = "AddressBookQueue"; // Ensure this matches the producer

        public RabbitMQConsumer()
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
        }

        public void StartConsuming()
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[x] Received: {message}");
            };

            channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            Console.WriteLine("Waiting for messages...");
            Thread.Sleep(Timeout.Infinite); // Keeps the consumer running
        }

        public void PublishMessage(string queueName, string message)
        {
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: queueName,
                    basicProperties: null,
                    body: body
                );
            }
        }

    }
}
