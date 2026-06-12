using Cms.Application.Common.Interfaces;
using Cms.Infrastructure.Common.Services;
using Cms.Worker;
using RabbitMQ.Client;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, service) =>
    {
        service.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = context.Configuration.GetConnectionString("Redis");
        });

        service.AddScoped<ICacheService, RedisCacheService>();

        service.AddSingleton<IConnection>(sp =>
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(context.Configuration.GetConnectionString("RabbitMQ")!)
            };
            return factory.CreateConnection();
        });

        service.AddHostedService<ContentEventWorker>();
    });