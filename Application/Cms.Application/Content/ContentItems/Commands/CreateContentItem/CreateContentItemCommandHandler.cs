

using Cms.Application.Content.Interfaces;
using Cms.Domain.Content.ContentItems;
using MediatR;

namespace Cms.Application.Content.ContentItems.Commands.CreateContentItem;

public class CreateContentItemCommandHandler : IRequestHandler<CreateContentItemCommand, Guid>
{
    private readonly IContentItemRepository _repo;
    private readonly IContentTypeRepository _contentTypeRepository;

    public CreateContentItemCommandHandler(IContentItemRepository repo, IContentTypeRepository contentTypeRepository)
    {
        _repo = repo;
        _contentTypeRepository = contentTypeRepository;
    }

    public async Task<Guid> Handle(CreateContentItemCommand request, CancellationToken cancellationToken)
    {

        var contentType = await _contentTypeRepository.GetByIdAsync(request.ContentTypeId);
        if (contentType == null)
            throw new Exception("Content type not found");

        foreach (var def in contentType.FieldDefinitions)
        {
            var value = request.Values.FirstOrDefault(v => v.FieldName == def.Name);

            if (value != null)
            {
                switch (def.Type)
                {
                    case Domain.Content.ContentTypes.FieldType.Number:
                        if (!int.TryParse(value.Value, out _))
                        {
                            throw new Exception($"Field {def.Name} must be a number");
                        }
                    break;

                    case Domain.Content.ContentTypes.FieldType.Boolean:
                        if (!bool.TryParse(value.Value, out _))
                            throw new Exception($"Field '{def.Name}' must be a boolean.");
                        break;

                    case Domain.Content.ContentTypes.FieldType.Date:
                        if (!DateTime.TryParse(value.Value, out _))
                            throw new Exception($"Field '{def.Name}' must be a valid date.");
                        break;

                    case Domain.Content.ContentTypes.FieldType.Reference:
                        if (!Guid.TryParse(value.Value, out _))
                            throw new Exception($"Field '{def.Name}' must be a valid GUID.");
                        break;
                }
            }
        }

        var item = ContentItem.Create(request.ContentTypeId, request.Slug);

        if (!string.IsNullOrWhiteSpace(request.Title))
            item.AddValue(new ContentFieldValue("Title", request.Title));

        foreach (var v in request.Values)
            item.AddValue(new ContentFieldValue(v.FieldName, v.Value));

        await _repo.AddAsync(item);
        await _repo.SaveChangesAsync();

        return item.Id;
    }
}

