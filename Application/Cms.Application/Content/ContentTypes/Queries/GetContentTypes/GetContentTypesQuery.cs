

using MediatR;

namespace Cms.Application.Content.ContentTypes.Queries.GetContentTypes;

public record GetContentTypesQuery() : IRequest<List<ContentTypeDto>>;

public record ContentTypeDto(
    Guid Id,
    string Name,
    string Slug,
    string Description
    );