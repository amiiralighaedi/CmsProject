// ContentItem.cs
using Cms.Shared.Domain.Entities;

namespace Cms.Domain.Content.ContentItems;

public class ContentItem : BaseEntity, IAggregateRoot
{
    public Guid Id { get; private set; }
    public Guid ContentTypeId { get; private set; }

    private readonly List<ContentFieldValue> _values = new();
    public IReadOnlyList<ContentFieldValue> Values => _values.AsReadOnly();

    private readonly List<ContentVersion> _versions = new();
    public IReadOnlyList<ContentVersion> Versions => _versions.AsReadOnly();

    public ContentStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private ContentItem() { }

    private ContentItem(Guid id, Guid contentTypeId)
    {
        Id = id;
        ContentTypeId = contentTypeId;
        Status = ContentStatus.Draft;
        CreatedAt = DateTime.UtcNow;
    }

    public static ContentItem Create(Guid contentTypeId)
        => new(Guid.NewGuid(), contentTypeId);

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

    public void Publish()
    {
        if (Status == ContentStatus.Published)
            return;

        Status = ContentStatus.Published;

        var version = ContentVersion.Create(this);
        _versions.Add(version);
        UpdatedAt = DateTime.UtcNow;
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
