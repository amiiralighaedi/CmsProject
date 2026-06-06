

using Cms.Application.Content.DTOs;

namespace Cms.Application.Content.Interfaces;

public interface IContentQueryRepository
{
    Task<List<ContentItemDto>> GetListAsync(string contentType, int page, int pageSize);
    Task<ContentItemDto?> GetByIdAsync(string contentType, Guid id);
    Task<ContentItemDto?> GetBySlugAsync(string contentType, string slug);
}