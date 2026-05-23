
using MediatR;

namespace Cms.Application.Content.ContentItems.Queries;

public record GetByIdContentItemsQuery(Guid Id) : IRequest<ContentItemDto>;

public record ContentItemDto(
    Guid Id,
    Guid ContentTypeId,
    string Status,
    List<ContentFieldValueDtoGet> Values,
    List<ContentVersionDto> Versions
    );


public record ContentFieldValueDtoGet(string FieldName, string? Value);

public record ContentVersionDto(int VersionNumber, DateTime CreatedAt);