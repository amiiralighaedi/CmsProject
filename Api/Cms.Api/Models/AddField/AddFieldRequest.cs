using Cms.Domain.Content.ContentTypes;

namespace Cms.Api.Models.AddField;

public class AddFieldRequest
{
    public string Name { get; set; } = default!;
    public FieldType Type { get; set; }
    public bool IsRequired { get; set; }
}
