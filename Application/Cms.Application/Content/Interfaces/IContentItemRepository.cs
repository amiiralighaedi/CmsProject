// IContentItemRepository.cs
using Cms.Domain.Content.ContentItems;

namespace Cms.Application.Content.Interfaces;

public interface IContentItemRepository
{
    Task AddAsync(ContentItem contentItem);
    Task<ContentItem?> GetByIdAsync(Guid id);
    IQueryable<ContentItem> Query();
    void Remove(ContentItem contentItem);
    Task SaveChangesAsync();
}
