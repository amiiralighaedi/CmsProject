

using Cms.Application.Content.Interfaces;
using Cms.Domain.Content.ContentTypes;
using MediatR;

namespace Cms.Application.Content.ContentTypes.Commands.AddFieldContentType;

public class AddFieldToContentTypeCommandHandler : IRequestHandler<AddFieldToContentTypeCommand, Unit>
{
    private readonly IContentTypeRepository _repo;

    public AddFieldToContentTypeCommandHandler(IContentTypeRepository repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(AddFieldToContentTypeCommand request, CancellationToken cancellationToken)
    {
        var type = await _repo.GetByIdAsync(request.ContentTypeId);
        if (type == null)
            throw new Exception("ContentType not found");

        var field = new ContentFieldDefinition(request.Name, request.Type, request.IsRequired);

        type.AddField(field);

        await _repo.SaveChangesAsync();

        return Unit.Value;
    }

}