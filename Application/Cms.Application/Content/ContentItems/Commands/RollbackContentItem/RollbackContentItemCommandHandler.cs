

using Cms.Application.Content.Interfaces;
using MediatR;

namespace Cms.Application.Content.ContentItems.Commands.RollbackContentItem;

public class RollbackContentItemCommandHandler : IRequestHandler<RollbackContentItemCommand, Unit>
{
    private readonly IContentItemRepository _itemRepo;

    public RollbackContentItemCommandHandler(IContentItemRepository itemRepo)
    {
        _itemRepo = itemRepo;
    }

    public async Task<Unit> Handle(RollbackContentItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemRepo.GetByIdAsync(request.Id);
        if (item == null)
            throw new Exception("ContentItem not found");

        item.RollbackToVersion(request.VersionNumber);

        await _itemRepo.SaveChangesAsync();

        return Unit.Value;
    }
}
