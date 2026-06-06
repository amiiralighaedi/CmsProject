
namespace Cms.Application.Content.DTOs;

public class ContentItemDto
{
    public Guid Id { get; set; }
    public string ContentType { get; set; } = default!;
    public string? Slug { get; set; }
    public DateTime PublishedAt { get; set; }
    public List<ContentFieldDto> Fields { get; set; } = new();
}
