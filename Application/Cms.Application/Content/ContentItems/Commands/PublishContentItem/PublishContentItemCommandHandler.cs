

using Cms.Application.Content.Interfaces;
using MediatR;

namespace Cms.Application.Content.ContentItems.Commands.PublishContentItem;

public class PublishContentItemCommandHandler : IRequestHandler<PublishContentItemCommand, Unit>
{
    private readonly IContentItemRepository _itemRepo;

    public PublishContentItemCommandHandler(IContentItemRepository itemRepo)
    {
        _itemRepo = itemRepo;
    }

    public async Task<Unit> Handle(PublishContentItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemRepo.GetByIdAsync(request.Id);
        if (item == null)
            throw new Exception("Content Item not found");

        item.Publish();

        await _itemRepo.SaveChangesAsync();

        return Unit.Value;
    }
}
