

namespace Cms.Shared.Abstractions;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
