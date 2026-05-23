namespace Cms.Shared.Domain.Entities;

public abstract class BaseEntity
{
    private readonly List<DomainEvent> _domainEvents = new();

    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow; 
    public DateTime? UpdatedAt { get; protected set; }

    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}