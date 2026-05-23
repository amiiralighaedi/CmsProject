

using MediatR;

namespace Cms.Application.Content.ContentItems.Queries.GetContentItemByVersion;

public record GetContentItemByVersionQuery(
    Guid ContentItemId,
    int VersionNumber
) : IRequest<ContentItemVersionDetailDto>;

public record ContentItemVersionDetailDto(
    int VersionNumber,
    DateTime CreatedAt,
    List<ContentItemVersionFieldDto> Fields
);

public record ContentItemVersionFieldDto(
    string FieldName,
    string? Value
);