using Cms.Application.Auth.Interfaces;
using Cms.Application.Common.Interfaces;
using Cms.Application.Content.Interfaces;
using Cms.Application.Media.Interfaces;
using Cms.Application.Media.Interfacesp;
using Cms.Infrastructure.Auth;
using Cms.Infrastructure.Common.Services;
using Cms.Infrastructure.Content.Repositories;
using Cms.Infrastructure.Events;
using Cms.Infrastructure.Media.Repositories;
using Cms.Infrastructure.Media.Services;
using Cms.Infrastructure.Persistence;
using Cms.Infrastructure.Search;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using RabbitMQ.Client;

namespace Cms.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseNpgsql(connectionString);
            opt.EnableSensitiveDataLogging();
            opt.LogTo(Console.WriteLine);
        });

        // Redis
        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = configuration.GetConnectionString("Redis");
        });

        services.AddScoped<ICacheService, RedisCacheService>();

        // RabbitMQ Connection
        services.AddSingleton<IConnection>(sp =>
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(configuration.GetConnectionString("RabbitMQ")!)
            };

            return factory.CreateConnection();
        });

        // Elastic Search
        services.AddSingleton<IElasticClient>(sp =>
        {
            var settings = new ConnectionSettings(
                new Uri(configuration.GetConnectionString("ElasticSearch")!)
            ).DefaultIndex("cms-content");

            return new ElasticClient(settings);
        });

        // Event Publisher
        services.AddScoped<IEventPublisher, RabbitMqEventPublisher>();

        // Repositories
        services.AddScoped<IContentTypeRepository, ContentTypeRepository>();
        services.AddScoped<IContentItemRepository, ContentItemRepository>();
        services.AddScoped<IMediaRepository, MediaRepository>();
        services.AddScoped<IMediaReadRepository, MediaReadRepository>();
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped<ISearchIndexService, ElasticSearchIndexService>();

        // Auth
        services.AddScoped<IAuthService, AuthService>();

        // Query Repository + Cache Decorator
        services.AddScoped<ContentQueryRepository>();
        services.AddScoped<IContentQueryRepository>(sp =>
            new CachedContentQueryRepository(
                sp.GetRequiredService<ContentQueryRepository>(),
                sp.GetRequiredService<ICacheService>()
            )
        );

        return services;
    }
}
