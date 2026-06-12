using Cms.Application.Common.Interfaces;
using Cms.Application.Content.DTOs;
using Cms.Application.Content.Interfaces;
using Cms.Application.Content.Queries;

namespace Cms.Infrastructure.Content.Repositories;

public class CachedContentQueryRepository : IContentQueryRepository
{
    private readonly IContentQueryRepository _inner;
    private readonly ICacheService _cache;

    public CachedContentQueryRepository(
        IContentQueryRepository inner,
        ICacheService cache)
    {
        _inner = inner;
        _cache = cache;
    }

    public async Task<ContentItemDto?> GetByIdAsync(string contentType, Guid id)
    {
        var key = $"content:item:{contentType}:{id}";

        var cached = await _cache.GetAsync<ContentItemDto>(key);
        if (cached != null)
            return cached;

        var result = await _inner.GetByIdAsync(contentType, id);
        if (result != null)
            await _cache.SetAsync(key, result);

        return result;
    }

    public async Task<ContentItemDto?> GetBySlugAsync(string contentType, string slug)
    {
        var key = $"content:slug:{contentType}:{slug}";

        var cached = await _cache.GetAsync<ContentItemDto>(key);
        if (cached != null)
            return cached;

        var result = await _inner.GetBySlugAsync(contentType, slug);
        if (result != null)
            await _cache.SetAsync(key, result);

        return result;
    }

    public async Task<List<ContentItemDto>> GetListAsync(string contentType, int page, int pageSize)
    {
        var key = $"content:list:{contentType}:{page}:{pageSize}";

        var cached = await _cache.GetAsync<List<ContentItemDto>>(key);
        if (cached != null)
            return cached;

        var result = await _inner.GetListAsync(contentType, page, pageSize);
        await _cache.SetAsync(key, result);

        return result;
    }

    public async Task<QueryResponse<object>> QueryAsync(
        string contentType,
        Dictionary<string, string>? filters,
        string? sort,
        bool desc,
        int page,
        int pageSize)
    {
        // ❗ Query API معمولاً cache نمی‌شود
        // چون فیلترها و sort و search زیاد هستند
        return await _inner.QueryAsync(contentType, filters, sort, desc, page, pageSize);
    }
}
