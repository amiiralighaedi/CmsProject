using Cms.Application.Common.Interfaces;
using Cms.Application.Common.Models;
using Cms.Application.Content.Queries;
using Nest;

namespace Cms.Infrastructure.Search;

public class ElasticSearchIndexService : ISearchIndexService
{
    private readonly IElasticClient _client;

    public ElasticSearchIndexService(IElasticClient client)
    {
        _client = client;
    }

    public async Task IndexContentAsync(SearchDocument document)
    {
        await _client.IndexAsync(document, idx => idx
            .Index("cms-content")
            .Id(document.Id)
        );
    }

    public async Task DeleteContentAsync(string id)
    {
        await _client.DeleteAsync<SearchDocument>(id, idx => idx
            .Index("cms-content")
        );
    }

    public async Task<QueryResponse<object>> SearchAsync(string contentType, QueryRequest request)
    {
        var response = await _client.SearchAsync<SearchDocument>(s => s
            .Index("cms-content")
            .Query(q => q
                .Bool(b => b
                    .Must(
                        m => m.Match(t => t
                            .Field(f => f.Body)
                            .Query(request.Search)
                        ),
                        m => m.Term(t => t
                            .Field(f => f.ContentType)
                            .Value(contentType)
                        )
                    )
                )
            )
            .From((request.Page - 1) * request.PageSize)
            .Size(request.PageSize)
        );

        return new QueryResponse<object>
        {
            Total = (int)response.Total,
            Page = request.Page,
            PageSize = request.PageSize,
            Items = response.Documents
        };
    }
}
