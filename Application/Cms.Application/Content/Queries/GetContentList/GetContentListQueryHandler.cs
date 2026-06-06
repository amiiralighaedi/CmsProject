

using Cms.Application.Content.DTOs;
using Cms.Application.Content.Interfaces;
using MediatR;

namespace Cms.Application.Content.Queries.GetContentList;

public class GetContentListQueryHandler : IRequestHandler<GetContentListQuery, List<ContentItemDto>>
{
    private readonly IContentQueryRepository _contentQueryRepository;

    public GetContentListQueryHandler(IContentQueryRepository contentQueryRepository)
    {
        _contentQueryRepository = contentQueryRepository;
    }

    public async Task<List<ContentItemDto>> Handle(GetContentListQuery request, CancellationToken cancellationToken)
    {
        return await _contentQueryRepository.GetListAsync(request.ContentType, request.Page, request.PageSize);
    }
}
