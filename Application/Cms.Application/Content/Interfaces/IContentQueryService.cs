

using Cms.Application.Content.Queries;

namespace Cms.Application.Content.Interfaces;

public interface IContentQueryService
{
    Task<QueryResponse<object>> QueryAsync(string contentType, QueryRequest request);
}
