
using Cms.Application.Content.Interfaces;
using Cms.Domain.Content.ContentTypes;
using MediatR;

namespace Cms.Application.Content.ContentItems.Commands.UpdateContentItem;

public class UpdateContentItemCommandHandler : IRequestHandler<UpdateContentItemCommand, Unit>
{
    private readonly IContentItemRepository _itemRepo;
    private readonly IContentTypeRepository _contentRepo;

    public UpdateContentItemCommandHandler(IContentItemRepository itemRepo, IContentTypeRepository contentRepo)
    {
        _itemRepo = itemRepo;
        _contentRepo = contentRepo;
    }

    public async Task<Unit> Handle(UpdateContentItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemRepo.GetByIdAsync(request.Id);
        if (item == null)
            throw new Exception("Content item not found");

        var type = await _contentRepo.GetByIdAsync(item.ContentTypeId);
        if (type == null)
            throw new Exception("Content type not found");

        foreach (var def in type.FieldDefinitions)
        {
            var value = request.Values.FirstOrDefault(v => v.FieldName == def.Name);

            if (def.IsRequired && value == null)
                throw new Exception($"Field '{def.Name}' is required.");

            if (value != null)
            {
                switch (def.Type)
                {
                    case FieldType.Number:
                        if (!int.TryParse(value.Value, out _))
                            throw new Exception($"Field '{def.Name}' must be a number.");
                        break;

                    case FieldType.Boolean:
                        if (!bool.TryParse(value.Value, out _))
                            throw new Exception($"Field '{def.Name}' must be a boolean.");
                        break;

                    case FieldType.Date:
                        if (!DateTime.TryParse(value.Value, out _))
                            throw new Exception($"Field '{def.Name}' must be a valid date.");
                        break;

                    case FieldType.Reference:
                        if (!Guid.TryParse(value.Value, out _))
                            throw new Exception($"Field '{def.Name}' must be a valid GUID.");
                        break;
                }
            }
        }

        item.ClearValues();
        foreach (var v in request.Values)
        {
            item.AddValue(new Domain.Content.ContentItems.ContentFieldValue(v.FieldName, v.Value));
        }

        await _itemRepo.SaveChangesAsync();

        return Unit.Value;

    }
}
