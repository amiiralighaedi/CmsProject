// ContentTypeRepository.cs
using Cms.Application.Content.Interfaces;
using Cms.Domain.Content.ContentTypes;
using Cms.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Cms.Infrastructure.Content.Repositories;

public class ContentTypeRepository : IContentTypeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ContentTypeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(ContentType contentType)
    {
        await _dbContext.ContentTypes.AddAsync(contentType);
    }

    public async Task<bool> ExistisBySlugAsync(string slug)
    {
        return await _dbContext.ContentTypes.AnyAsync(x => x.Slug == slug);
    }

    public async Task<List<ContentType>> GetAllAsync()
    {
        return await _dbContext.ContentTypes.ToListAsync();
    }

    public async Task<ContentType?> GetByIdAsync(Guid id)
    {
        return await _dbContext.ContentTypes
            .Include(x => x.FieldDefinitions)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
