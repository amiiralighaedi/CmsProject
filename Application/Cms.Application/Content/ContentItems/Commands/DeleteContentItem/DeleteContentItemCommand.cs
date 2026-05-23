

using MediatR;

namespace Cms.Application.Content.ContentItems.Commands.DeleteContentItem;

public record DeleteContentItemCommand(
    Guid id
    ) : IRequest<Unit>;
