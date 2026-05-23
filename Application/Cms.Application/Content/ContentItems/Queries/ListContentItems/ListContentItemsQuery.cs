

using MediatR;

namespace Cms.Application.Content.ContentItems.Queries.ListContentItems;

public record ListContentItemsQuery(
    Guid? ContentTypeId,
    int Page = 1,
    int PageSize = 20
    ) : IRequest<PageContentItem1ListDto>;

public record PageContentItem1ListDto(
    int totalCount,
    int Page,
    int PageSize,
    List<ContentItem1Dto> Items
    );

public record ContentItem1Dto(
    Guid Id,
    Guid ContentTypeId,
    string Status,
    DateTime CreatedAt
);