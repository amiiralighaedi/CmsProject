

using Cms.Application.Common.Interfaces;
using Cms.Application.Content.Interfaces;
using MediatR;

namespace Cms.Application.Content.ContentItems.Commands.PublishContentItem;

public class PublishContentItemCommandHandler : IRequestHandler<PublishContentItemCommand, Unit>
{
    private readonly IContentItemRepository _itemRepo;
    private readonly ICacheService _cacheService;

    public PublishContentItemCommandHandler(IContentItemRepository itemRepo, ICacheService cacheService)
    {
        _itemRepo = itemRepo;
        _cacheService = cacheService;
    }

    public async Task<Unit> Handle(PublishContentItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemRepo.GetByIdAsync(request.Id);
        if (item == null)
            throw new Exception("Content Item not found");

        var version = item.Publish();

        if (version != null)
        {
            await _itemRepo.AddVersionAsync(version);
        }

        await _itemRepo.SaveChangesAsync();

        await _cacheService.DeleteAsync($"content:item:{item.ContentType.Slug}:{item.Id}");
        await _cacheService.DeleteAsync($"content:slug:{item.ContentType.Slug}:{item.Slug}");
        await _cacheService.DeleteAsync($"content:list:{item.ContentType.Slug}:*");

        return Unit.Value;
    }
}
