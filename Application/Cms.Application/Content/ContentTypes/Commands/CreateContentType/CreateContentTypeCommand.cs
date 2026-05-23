using MediatR;

namespace Cms.Application.Content.ContentTypes.Commands.CreateContentType;

public record CreateContentTypeCommand(
    string Name,
    string Slug,
    string? Description
    ) : IRequest<Guid>;
