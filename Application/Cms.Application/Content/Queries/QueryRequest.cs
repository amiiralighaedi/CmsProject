

namespace Cms.Application.Content.Queries;

public class QueryRequest
{
    public string? Search { get; set; }
    public Dictionary<string, string>? Filters { get; set; }
    public string? Sort { get; set; }
    public bool Desc { get; set; } = false;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
