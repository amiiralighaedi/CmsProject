

using Cms.Shared.Domain.Entities;

namespace Cms.Domain.Content.ContentTypes;

public class ContentType : BaseEntity, IAggregateRoot
{

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Slug { get; private set; }
    public string? Description { get; private set; }
    private ContentType(Guid id, string name, string slug, string? description)
    {
        Id = id;
        Name = name;
        Slug = slug;
        Description = description;
    }

    private readonly List<ContentFieldDefinition> _fieldDefinitions = new();
    public IReadOnlyList<ContentFieldDefinition> FieldDefinitions => _fieldDefinitions.AsReadOnly();

    private ContentType()
    {
        
    }

    public static ContentType Create(string name, string slug, string? description)
    {
        return new ContentType(Guid.NewGuid(), name, slug, description);
    }

    public void AddField(ContentFieldDefinition field)
    {
        if (_fieldDefinitions.Any(f => f.Name == field.Name))
        {
            throw new InvalidOperationException($"Field '{field.Name}' already exists.");
        }

        _fieldDefinitions.Add(field);
    }


}
