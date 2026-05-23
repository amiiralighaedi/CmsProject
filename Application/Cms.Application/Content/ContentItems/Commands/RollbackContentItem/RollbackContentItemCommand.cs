
using MediatR;

namespace Cms.Application.Content.ContentItems.Commands.RollbackContentItem;

public record RollbackContentItemCommand(
    Guid Id,
    int VersionNumber
    ) : IRequest<Unit>;
