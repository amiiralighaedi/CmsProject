using Cms.Application.Common.Events;
using Cms.Application.Common.Interfaces;
using Cms.Application.Common.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Cms.Worker;

public class ContentEventWorker : BackgroundService
{
    private readonly IConnection _connection;
    private readonly ILogger<ContentEventWorker> _logger;
    private readonly ICacheService _cacheService;
    private readonly ISearchIndexService _searchIndexService;

    public ContentEventWorker(
        IConnection connection,
        ILogger<ContentEventWorker> logger,
        ICacheService cacheService,
        ISearchIndexService searchIndexService)
    {
        _connection = connection;
        _logger = logger;
        _cacheService = cacheService;
        _searchIndexService = searchIndexService;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var channel = _connection.CreateModel();

        string[] queues =
        {
            nameof(ContentPublishedEvent),
            nameof(ContentUpdateEvent),
            nameof(ContentDeleteEvent)
        };

        foreach (var queue in queues)
        {
            channel.QueueDeclare(
                queue: queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (sender, args) =>
            {
                var json = Encoding.UTF8.GetString(args.Body.ToArray());
                _logger.LogInformation($"Event Received: {queue} -> {json}");

                switch (queue)
                {
                    case nameof(ContentPublishedEvent):
                        var published = JsonSerializer.Deserialize<ContentPublishedEvent>(json);
                        await HandlePublished(published!);
                        break;

                    case nameof(ContentUpdateEvent):
                        var updated = JsonSerializer.Deserialize<ContentUpdateEvent>(json);
                        await HandleUpdated(updated!);
                        break;

                    case nameof(ContentDeleteEvent):
                        var deleted = JsonSerializer.Deserialize<ContentDeleteEvent>(json);
                        await HandleDeleted(deleted!);
                        break;
                }
            };

            channel.BasicConsume(
                queue: queue,
                autoAck: true,
                consumer: consumer
            );
        }

        return Task.CompletedTask;
    }

    private async Task HandlePublished(ContentPublishedEvent evt)
    {
        _logger.LogInformation($"Handling Published Event for {evt.ContentItemId}");

        // اگر خواستی بعداً SearchDocument را کامل پر می‌کنیم
        var doc = new SearchDocument
        {
            Id = evt.ContentItemId.ToString(),
            ContentType = evt.ContentType,
            Slug = evt.Slug,
            Title = "",
            Body = ""
        };

        await _searchIndexService.IndexContentAsync(doc);

        await _cacheService.DeleteAsync($"content:item:{evt.ContentType}:{evt.ContentItemId}");
        await _cacheService.DeleteAsync($"content:slug:{evt.ContentType}:{evt.Slug}");
        await _cacheService.DeleteAsync($"content:list:{evt.ContentType}:*");
    }

    private async Task HandleUpdated(ContentUpdateEvent evt)
    {
        _logger.LogInformation($"Handling Updated Event for {evt.ContentItemId}");

        var doc = new SearchDocument
        {
            Id = evt.ContentItemId.ToString(),
            ContentType = evt.ContentType,
            Slug = evt.Slug,
            Title = "",
            Body = ""
        };

        await _searchIndexService.IndexContentAsync(doc);

        await _cacheService.DeleteAsync($"content:item:{evt.ContentType}:{evt.ContentItemId}");
        await _cacheService.DeleteAsync($"content:slug:{evt.ContentType}:{evt.Slug}");
        await _cacheService.DeleteAsync($"content:list:{evt.ContentType}:*");
    }

    private async Task HandleDeleted(ContentDeleteEvent evt)
    {
        _logger.LogInformation($"Handling Deleted Event for {evt.ContentItemId}");

        await _searchIndexService.DeleteContentAsync(evt.ContentItemId.ToString());

        await _cacheService.DeleteAsync($"content:item:{evt.ContentType}:{evt.ContentItemId}");
        await _cacheService.DeleteAsync($"content:slug:{evt.ContentType}:{evt.Slug}");
        await _cacheService.DeleteAsync($"content:list:{evt.ContentType}:*");
    }
}
