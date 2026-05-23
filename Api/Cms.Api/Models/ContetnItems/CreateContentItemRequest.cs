namespace Cms.Api.Models.ContetnItems;

public class CreateContentItemRequest
{
    public Guid ContentTypeId { get; set; }
    public string? Title { get; set; }
    public List<ContentFieldValueRequest> Values { get; set; } = new();

    public class ContentFieldValueRequest
    {
        public string FieldName { get; set; } = default!;
        public string? Value { get; set; }
    }
}