
using Cms.Application.Content.Interfaces;
using MediatR;

namespace Cms.Application.Content.ContentItems.Queries;

public class GetContentItemQueryHandler : IRequestHandler<GetByIdContentItemsQuery, ContentItemDto>
{
    private readonly IContentItemRepository _repo;

    public GetContentItemQueryHandler(IContentItemRepository repo)
    {
        _repo = repo;
    }

    public async Task<ContentItemDto> Handle(GetByIdContentItemsQuery request, CancellationToken cancellationToken)
    {
        var item = await _repo.GetByIdAsync(request.Id);

        if (item == null)
        {
            throw new Exception("ContentItem not found");
        }

        return new ContentItemDto(
            item.Id,
            item.ContentTypeId,
            item.Status.ToString(),
            item.Values.Select(v => new ContentFieldValueDtoGet(v.FieldName, v.Value)).ToList(),
            item.Versions.Select(v => new ContentVersionDto(v.VersionNumber, v.CreateAt)).ToList()
            );
    }
}
