// DependencyInjection.cs
using Cms.Application.Content.Interfaces;
using Cms.Application.Media.Interfaces;
using Cms.Application.Media.Interfacesp;
using Cms.Infrastructure.Content.Repositories;
using Cms.Infrastructure.Media.Repositories;
using Cms.Infrastructure.Media.Services;
using Cms.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        services.AddScoped<IContentTypeRepository, ContentTypeRepository>();
        services.AddScoped<IContentItemRepository, ContentItemRepository>();
        services.AddScoped<IMediaRepository, MediaRepository>();
        services.AddScoped<IMediaReadRepository, MediaReadRepository>();
        services.AddScoped<IFileStorageService, FileStorageService>();

        return services;
    }
}
