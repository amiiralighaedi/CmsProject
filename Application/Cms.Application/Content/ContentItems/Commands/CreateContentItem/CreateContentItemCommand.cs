

using MediatR;
using MediatR.Pipeline;

namespace Cms.Application.Content.ContentItems.Commands.CreateContentItem;

public record CreateContentItemCommand(
    Guid ContentTypeId,
    string? Title,
    List<ContentFieldValueDto>? Values
    ) : IRequest<Guid>;

public record ContentFieldValueDto(
    
    string FieldName,
    string? Value
    );

