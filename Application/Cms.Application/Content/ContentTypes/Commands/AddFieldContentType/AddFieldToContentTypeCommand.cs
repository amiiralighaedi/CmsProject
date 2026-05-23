

using Cms.Domain.Content.ContentTypes;
using MediatR;

namespace Cms.Application.Content.ContentTypes.Commands.AddFieldContentType;

public record AddFieldToContentTypeCommand(
    Guid ContentTypeId,
    string Name,
    FieldType Type,
    bool IsRequired
    ) : IRequest<Unit>;