

using MediatR;

namespace Cms.Application.Content.ContentItems.Commands.UpdateContentItem;

public record UpdateContentItemCommand(
    Guid Id,
    string? Title,
    List<UpdateFieldValueDto> Values

    ): IRequest<Unit>;

public record UpdateFieldValueDto(
    string FieldName,
    string? Value
    );