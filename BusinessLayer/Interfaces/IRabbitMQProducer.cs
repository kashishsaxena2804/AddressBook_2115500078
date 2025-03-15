using System;

namespace BusinessLayer.Interfaces
{
    public interface IRabbitMQProducer
    {
        void PublishMessage(string queueName, string message);
    }
}
