namespace Cms.Domain.Content.ContentItems;

public class ContentVersion
{
    public Guid Id { get; private set; }
    public Guid ContentItemId { get; private set; }
    public ContentItem ContentItem { get; private set; }

    public int VersionNumber { get; private set; }
    public DateTime CreateAt { get; private set; }

    private readonly List<ContentFieldValue> _values = new();
    //public IReadOnlyList<ContentFieldValue> Values => _values.AsReadOnly();
     public IReadOnlyCollection<ContentFieldValue> Values => _values;

    private ContentVersion() { }

    private ContentVersion(Guid contentItemId, int versionNumber)
    {
        Id = Guid.NewGuid();
        ContentItemId = contentItemId;
        VersionNumber = versionNumber;
        CreateAt = DateTime.UtcNow;
    }

    public static ContentVersion Create(ContentItem item)
    {
        var version = new ContentVersion(item.Id, item.Versions.Count + 1)
        {
            ContentItem = item
        };

        foreach (var v in item.Values)
        {
            version._values.Add(new ContentFieldValue(v.FieldName, v.Value));
        }

        return version;
    }
}
