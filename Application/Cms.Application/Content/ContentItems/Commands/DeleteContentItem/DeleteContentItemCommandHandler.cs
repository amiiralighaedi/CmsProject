

using Cms.Application.Content.Interfaces;
using MediatR;

namespace Cms.Application.Content.ContentItems.Commands.DeleteContentItem;

public class DeleteContentItemCommandHandler : IRequestHandler<DeleteContentItemCommand, Unit>
{
    private readonly IContentItemRepository _itemRepo;

    public DeleteContentItemCommandHandler(IContentItemRepository itemRepo)
    {
        _itemRepo = itemRepo;
    }

    public async Task<Unit> Handle(DeleteContentItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemRepo.GetByIdAsync(request.id);
        if (item == null)
            throw new Exception("Content Item not found");

        _itemRepo.Remove(item);

        await _itemRepo.SaveChangesAsync();

        return Unit.Value;
    }
}
