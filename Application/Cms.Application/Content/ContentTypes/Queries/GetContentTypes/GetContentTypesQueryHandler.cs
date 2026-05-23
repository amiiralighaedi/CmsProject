
using Cms.Application.Content.Interfaces;
using MediatR;

namespace Cms.Application.Content.ContentTypes.Queries.GetContentTypes;

public class GetContentTypesQueryHandler : IRequestHandler<GetContentTypesQuery, List<ContentTypeDto>>
{
    private readonly IContentTypeRepository _repo;

    public GetContentTypesQueryHandler(IContentTypeRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<ContentTypeDto>> Handle(GetContentTypesQuery request, CancellationToken cancellationToken)
    {
        var list = await _repo.GetAllAsync();

        return list.Select(x => new ContentTypeDto
        (
            
            x.Id,
            x.Name,
            x.Slug,
            x.Description
            )).ToList();

    }
}
