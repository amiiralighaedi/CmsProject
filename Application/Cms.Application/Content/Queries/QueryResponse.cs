

namespace Cms.Application.Content.Queries;

public class QueryResponse<T>
{
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
}
