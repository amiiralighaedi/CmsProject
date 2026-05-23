using Cms.Application.Content.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cms.Application.Content.ContentItems.Queries.ListContentItems;

public class ListContentItemsQueryHandler : IRequestHandler<ListContentItemsQuery, PageContentItem1ListDto>
{
    private readonly IContentItemRepository _itemRepo;

    public ListContentItemsQueryHandler(IContentItemRepository itemRepo)
    {
        _itemRepo = itemRepo;
    }

    public async Task<PageContentItem1ListDto> Handle(ListContentItemsQuery request, CancellationToken cancellationToken)
    {
        var query = _itemRepo.Query();

        if (request.ContentTypeId.HasValue)
            query = query.Where(x => x.ContentTypeId == request.ContentTypeId.Value);

        var total = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new ContentItem1Dto(
                x.Id,
                x.ContentTypeId,
                x.Status.ToString(),
                x.CreatedAt
            ))
            .ToListAsync(cancellationToken);

        return new PageContentItem1ListDto(
            total,
            request.Page,
            request.PageSize,
            items
        );
    }
}