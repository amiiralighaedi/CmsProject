
using Cms.Application.Content.DTOs;
using MediatR;

namespace Cms.Application.Content.Queries.GetContentList;

public record GetContentListQuery(string ContentType, int Page = 1, int PageSize = 20 )
    : IRequest<List<ContentItemDto>>;
