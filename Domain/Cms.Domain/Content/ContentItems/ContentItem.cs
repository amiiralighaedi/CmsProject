using Cms.Shared.Domain.Entities;
using Cms.Domain.Content.ContentTypes;

namespace Cms.Domain.Content.ContentItems;

public class ContentItem : BaseEntity, IAggregateRoot
{
    public Guid Id { get; private set; }

    // ارتباط با ContentType
    public Guid ContentTypeId { get; private set; }
    public ContentType ContentType { get; private set; } = default!;

    // Slug برای URL
    public string Slug { get; private set; } = default!;

    private readonly List<ContentFieldValue> _values = new();
    public IReadOnlyCollection<ContentFieldValue> Values => _values;

    private readonly List<ContentVersion> _versions = new();
    public IReadOnlyCollection<ContentVersion> Versions => _versions;

    public ContentStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private ContentItem() { }

    private ContentItem(Guid id, Guid contentTypeId, string slug)
    {
        Id = id;
        ContentTypeId = contentTypeId;
        Slug = slug;
        Status = ContentStatus.Draft;
        CreatedAt = DateTime.UtcNow;
    }

    public static ContentItem Create(Guid contentTypeId, string slug)
        => new(Guid.NewGuid(), contentTypeId, slug);

    public void AddValue(ContentFieldValue value)
    {
        _values.Add(value);
        UpdatedAt = DateTime.UtcNow;
    }

    public void ClearValues()
    {
        _values.Clear();
        UpdatedAt = DateTime.UtcNow;
    }

    public ContentVersion? Publish()
    {
        if (Status == ContentStatus.Published)
            return null;

        Status = ContentStatus.Published;

        var version = ContentVersion.Create(this);
        _versions.Add(version);

        UpdatedAt = DateTime.UtcNow;

        return version;
    }

    public void RollbackToVersion(int versionNumber)
    {
        var version = _versions.FirstOrDefault(v => v.VersionNumber == versionNumber);
        if (version == null)
            throw new Exception($"Version {versionNumber} not found.");

        _values.Clear();

        foreach (var v in version.Values)
        {
            _values.Add(new ContentFieldValue(v.FieldName, v.Value));
        }

        Status = ContentStatus.Draft;
        UpdatedAt = DateTime.UtcNow;
    }
}
