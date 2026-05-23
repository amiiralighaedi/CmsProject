using Cms.Application.Content.Interfaces;
using Cms.Domain.Content.ContentTypes;
using MediatR;

namespace Cms.Application.Content.ContentTypes.Commands.CreateContentType;

public class CreateContentTypeCommandHandler : IRequestHandler<CreateContentTypeCommand, Guid>
{
    private readonly IContentTypeRepository _repo;

    public CreateContentTypeCommandHandler(IContentTypeRepository repo)
    {
        _repo = repo;
    }

    public async Task<Guid> Handle(CreateContentTypeCommand request, CancellationToken cancellationToken)
    {
        var exist = await _repo.ExistisBySlugAsync(request.Slug);
        if (exist)
            throw new Exception("Slug already exists.");

        var contentType = ContentType.Create(
            request.Name,
            request.Slug,
            request.Description
        );

        await _repo.AddAsync(contentType);
        await _repo.SaveChangesAsync();

        return contentType.Id;
    }
}
