

using MediatR;

namespace Cms.Application.Content.ContentItems.Queries.GetContentItemVersions;

public record GetContentItemVersionsQuery(Guid ContentItemId) : IRequest<List<ContentItemVersionDto>>;

public record ContentItemVersionDto(
    int VersionNumber,
    DateTime CreateAt
    ); 
