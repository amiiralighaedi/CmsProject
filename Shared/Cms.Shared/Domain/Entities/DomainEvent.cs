

namespace Cms.Shared.Domain.Entities;

public abstract class DomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}