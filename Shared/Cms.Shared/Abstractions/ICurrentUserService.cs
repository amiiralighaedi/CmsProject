

namespace Cms.Shared.Abstractions;

public interface ICurrentUserService
{
    Guid? UserId { get; }
}
