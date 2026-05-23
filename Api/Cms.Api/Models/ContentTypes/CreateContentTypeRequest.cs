namespace Cms.Api.Models.ContentTypes;

public class CreateContentTypeRequest
{
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
}
