

namespace Cms.Application.Common.Models;

public class SearchDocument
{
    public string Id { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Body { get; set; } = default!;
    public Dictionary<string, string> Fields { get; set; } = new();
}
