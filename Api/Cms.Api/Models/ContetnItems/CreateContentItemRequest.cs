namespace Cms.Api.Models.ContetnItems;

public class CreateContentItemRequest
{
    public Guid ContentTypeId { get; set; }
    public string? Title { get; set; }
    public string Slug { get; set; } = default!;
    public List<ContentFieldValueRequest> Values { get; set; } = new();

    public class ContentFieldValueRequest
    {
        public string FieldName { get; set; } = default!;
        public string? Value { get; set; }
    }
}