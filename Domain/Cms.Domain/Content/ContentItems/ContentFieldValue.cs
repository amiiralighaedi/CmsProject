using Cms.Shared.Domain.ValueObjects;

namespace Cms.Domain.Content.ContentItems;

public class ContentFieldValue : ValueObject
{
    public string FieldName { get; }
    public string? Value { get; }

    public ContentFieldValue(string fieldName, string? value)
    {
        FieldName = fieldName;
        Value = value;
    }

    private ContentFieldValue() { }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return FieldName;
        yield return Value;
    }
}
