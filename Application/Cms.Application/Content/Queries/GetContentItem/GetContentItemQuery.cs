

using Cms.Application.Content.DTOs;
using MediatR;

namespace Cms.Application.Content.Queries.GetContentItem;

public record GetContentItemQuery(string ContentType, Guid? Id = null, string? Slug = null)
    : IRequest<ContentItemDto?>;
