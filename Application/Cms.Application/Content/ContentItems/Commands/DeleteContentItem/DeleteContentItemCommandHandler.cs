

using Cms.Application.Common.Interfaces;
using Cms.Application.Content.Interfaces;
using MediatR;

namespace Cms.Application.Content.ContentItems.Commands.DeleteContentItem;

public class DeleteContentItemCommandHandler : IRequestHandler<DeleteContentItemCommand, Unit>
{
    private readonly IContentItemRepository _itemRepo;
    private readonly ICacheService _cache;

    public DeleteContentItemCommandHandler(IContentItemRepository itemRepo, ICacheService cache)
    {
        _itemRepo = itemRepo;
        _cache = cache;
    }

    public async Task<Unit> Handle(DeleteContentItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemRepo.GetByIdAsync(request.id);
        if (item == null)
            throw new Exception("Content Item not found");

        _itemRepo.Remove(item);

        await _itemRepo.SaveChangesAsync();

        await _cache.DeleteAsync($"content:item:{item.ContentType.Slug}:{item.Id}");
        await _cache.DeleteAsync($"content:slug:{item.ContentType.Slug}:{item.Slug}");
        await _cache.DeleteAsync($"content:list:{item.ContentType.Slug}:*");

        return Unit.Value;
    }
}
