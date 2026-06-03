
using Cms.Application.Media.Interfaces;
using Cms.Domain.Media.MediaItems;
using Cms.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Cms.Infrastructure.Media.Repositories;

public class MediaRepository : IMediaRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MediaRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(MediaItem mediaItem)
    {
        await _dbContext.MediaItems.AddAsync(mediaItem);
    }

    public async Task DeleteAsync(MediaItem mediaItem)
        => _dbContext.MediaItems.Remove(mediaItem);

    public async Task<MediaItem?> GetByIsAsync(Guid id)
    {
        return await _dbContext.MediaItems.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
