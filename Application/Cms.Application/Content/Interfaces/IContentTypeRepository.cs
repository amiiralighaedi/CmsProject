// IContentTypeRepository.cs
using Cms.Domain.Content.ContentTypes;

namespace Cms.Application.Content.Interfaces;

public interface IContentTypeRepository
{
    Task AddAsync(ContentType contentType);
    Task<bool> ExistisBySlugAsync(string slug);
    Task<List<ContentType>> GetAllAsync();
    Task<ContentType?> GetByIdAsync(Guid id);
    Task SaveChangesAsync();
}
