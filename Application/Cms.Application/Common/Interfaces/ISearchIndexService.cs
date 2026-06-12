using Cms.Application.Common.Models;
using Cms.Application.Content.Queries;

namespace Cms.Application.Common.Interfaces;

public interface ISearchIndexService
{
    Task IndexContentAsync(SearchDocument document);
    Task DeleteContentAsync(string id);
    Task<QueryResponse<object>> SearchAsync(string contentType, QueryRequest request);
}
