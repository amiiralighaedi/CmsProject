using Cms.Application.Common.Interfaces;
using System.Text;
using System.Text.Json;

namespace Cms.Infrastructure.Events;

public class RabbitMqEventPublisher : IEventPublisher
{
    private readonly RabbitMQ.Client.IConnection _connection;

    public RabbitMqEventPublisher(RabbitMQ.Client.IConnection connection)
    {
        _connection = connection;
    }

    public Task PublishAsync<T>(T @event) where T : class
    {
        using var channel = _connection.CreateModel();

        var queue = typeof(T).Name;

        channel.QueueDeclare(
            queue: queue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var json = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(
            exchange: "",
            routingKey: queue,
            mandatory: false,
            basicProperties: null,
            body: body
        );

        return Task.CompletedTask;
    }
}
