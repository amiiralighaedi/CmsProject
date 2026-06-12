

using Cms.Application.Content.DTOs;
using Cms.Application.Content.Queries;

namespace Cms.Application.Content.Interfaces;

public interface IContentQueryRepository
{
    Task<List<ContentItemDto>> GetListAsync(string contentType, int page, int pageSize);
    Task<ContentItemDto?> GetByIdAsync(string contentType, Guid id);
    Task<ContentItemDto?> GetBySlugAsync(string contentType, string slug);
    Task<QueryResponse<object>> QueryAsync(
      string contentType,
      Dictionary<string, string>? filters,
      string? sort,
      bool desc,
      int page,
      int pageSize);
}