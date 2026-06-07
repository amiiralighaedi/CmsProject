

using Cms.Application.Common.Interfaces;
using Cms.Application.Content.DTOs;
using Cms.Application.Content.Interfaces;
using System.Reflection;

namespace Cms.Infrastructure.Content.Repositories;

public class CachedContentQueryRepository : IContentQueryRepository
{
    private readonly IContentQueryRepository _contentQueryRepository;
    private readonly ICacheService _cacheService;

    public CachedContentQueryRepository(IContentQueryRepository contentQueryRepository, ICacheService cacheService)
    {
        _contentQueryRepository = contentQueryRepository;
        _cacheService = cacheService;
    }

    public async Task<ContentItemDto?> GetByIdAsync(string contentType, Guid id)
    {
        var cacheKey = $"content:item:{contentType}:{id}";
        var cached = await _contentQueryRepository.GetByIdAsync(contentType, id);
        if (cached != null)
            return cached;
        var res = await _contentQueryRepository.GetByIdAsync(contentType, id);
        if (res != null)
            await _cacheService.SetAsync(cacheKey, res);

        return res;
    }

    public async Task<ContentItemDto?> GetBySlugAsync(string contentType, string slug)
    {
        var key = $"content:slug:{contentType}:{slug}";

        var cached = await _cacheService.GetAsync<ContentItemDto>(key);
        if (cached != null)
            return cached;

        var result = await _contentQueryRepository.GetBySlugAsync(contentType, slug);
        if (result != null)
            await _cacheService.SetAsync(key, result);

        return result;
    }

    public async Task<List<ContentItemDto>> GetListAsync(string contentType, int page, int pageSize)
    {
        var key = $"content:list:{contentType}:{page}:{pageSize}";

        var cached = await _cacheService.GetAsync<List<ContentItemDto>>(key);
        if (cached != null)
            return cached;

        var result = await _contentQueryRepository.GetListAsync(contentType, page, pageSize);
        await _cacheService.SetAsync(key, result);

        return result;
    }
}
