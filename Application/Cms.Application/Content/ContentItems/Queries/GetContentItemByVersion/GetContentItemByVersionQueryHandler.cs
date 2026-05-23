

using Cms.Application.Content.Interfaces;
using MediatR;

namespace Cms.Application.Content.ContentItems.Queries.GetContentItemByVersion;

public class GetContentItemByVersionQueryHandler : IRequestHandler<GetContentItemByVersionQuery, ContentItemVersionDetailDto>
{

    private readonly IContentItemRepository _itemRepo;

    public GetContentItemByVersionQueryHandler(IContentItemRepository itemRepo)
    {
        _itemRepo = itemRepo;
    }

    public async Task<ContentItemVersionDetailDto> Handle(GetContentItemByVersionQuery request, CancellationToken cancellationToken)
    {
        var item = await _itemRepo.GetByIdAsync(request.ContentItemId);
        if (item == null)
            throw new Exception("Content Item not found");

        var version = item.Versions.FirstOrDefault(v => v.VersionNumber == request.VersionNumber);
        if (version == null)
            throw new Exception($"Version {request.VersionNumber} not found");

        var fields = version.Values
          .Select(v => new ContentItemVersionFieldDto(v.FieldName, v.Value))
          .ToList();

        return new ContentItemVersionDetailDto(
        version.VersionNumber,
        version.CreateAt,
        fields
    );
    }
}
