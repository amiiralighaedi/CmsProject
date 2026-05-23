

using Cms.Application.Content.Interfaces;
using MediatR;

namespace Cms.Application.Content.ContentItems.Queries.GetContentItemVersions;

public class GetContentItemVersionsQueryHandler : IRequestHandler<GetContentItemVersionsQuery, List<ContentItemVersionDto>>
{
    private readonly IContentItemRepository _itemRepo;

    public GetContentItemVersionsQueryHandler(IContentItemRepository itemRepo)
    {
        _itemRepo = itemRepo;
    }

    public async Task<List<ContentItemVersionDto>> Handle(GetContentItemVersionsQuery request, CancellationToken cancellationToken)
    {
        var item = await _itemRepo.GetByIdAsync(request.ContentItemId);
        if (item == null)
            throw new Exception($"Content item with id {request.ContentItemId} not found.");

        return item.Versions
            .OrderByDescending(v => v.VersionNumber)
            .Select(v => new ContentItemVersionDto(
                v.VersionNumber,
                v.CreateAt
                )).ToList();
    }
}
