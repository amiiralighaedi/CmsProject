

using MediatR;

namespace Cms.Application.Content.ContentItems.Commands.PublishContentItem;

public record PublishContentItemCommand(
    Guid Id
    ): IRequest<Unit>;

