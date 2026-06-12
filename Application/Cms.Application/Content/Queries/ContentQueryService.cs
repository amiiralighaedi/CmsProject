

using Cms.Application.Common.Interfaces;
using Cms.Application.Content.Interfaces;

namespace Cms.Application.Content.Queries;

public class ContentQueryService : IContentQueryService
{
    private readonly IContentQueryRepository _repo;
    private readonly ISearchIndexService _searchIndexService;

    public ContentQueryService(IContentQueryRepository repo, ISearchIndexService searchIndexService)
    {
        _repo = repo;
        _searchIndexService = searchIndexService;
    }

    public async Task<QueryResponse<object>> QueryAsync(string contentType, QueryRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            return await _searchIndexService.SearchAsync(contentType, request);
        }

        return await _repo.QueryAsync(
            contentType,
            request.Filters,
            request.Sort,
            request.Desc,
            request.Page,
            request.PageSize
            );
    }
    
}
