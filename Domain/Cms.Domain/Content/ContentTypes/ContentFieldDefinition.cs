

using Cms.Shared.Domain.ValueObjects;

namespace Cms.Domain.Content.ContentTypes;

public class ContentFieldDefinition : ValueObject
{
    public ContentFieldDefinition(string name, FieldType type, bool isRequired)
    {
        Name = name;
        Type = type;
        IsRequired = isRequired;
    }

    public string Name { get; }
    public FieldType Type { get; set; }
    public bool IsRequired { get; set; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Name;
        yield return Type;
        yield return IsRequired;
    }
}
