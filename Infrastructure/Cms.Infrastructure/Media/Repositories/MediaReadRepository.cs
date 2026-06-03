

using Cms.Application.Media.Interfaces;
using Cms.Domain.Media.MediaItems;
using Cms.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Cms.Infrastructure.Media.Repositories;

public class MediaReadRepository : IMediaReadRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MediaReadRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<MediaItem>> ListAsync()
    {
        return await _dbContext.MediaItems.ToListAsync();
    }
}
