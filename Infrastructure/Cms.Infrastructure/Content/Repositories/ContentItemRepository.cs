using Cms.Application.Content.Interfaces;
using Cms.Domain.Content.ContentItems;
using Cms.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Cms.Infrastructure.Content.Repositories;

public class ContentItemRepository : IContentItemRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ContentItemRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(ContentItem contentItem)
    {
        await _dbContext.ContentItems.AddAsync(contentItem);
    }

    public async Task AddVersionAsync(ContentVersion version)
    {
        await _dbContext.Set<ContentVersion>().AddAsync(version);
    }

    public async Task<ContentItem?> GetByIdAsync(Guid id)
    {
        // ❗ نکته مهم:
        // Values به‌عنوان JSON-owned خودکار لود می‌شود؛ Include لازم نیست
        return await _dbContext.ContentItems
            .Include(x => x.Versions) // Versions لازم است
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<ContentItem> Query()
    {
        return _dbContext.ContentItems.AsQueryable();
    }

    public void Remove(ContentItem contentItem)
    {
        _dbContext.ContentItems.Remove(contentItem);
    }

    public async Task SaveChangesAsync()
    {
        Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);

        await _dbContext.SaveChangesAsync();
    }
}
