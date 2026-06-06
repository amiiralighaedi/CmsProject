using Cms.Application.Content.DTOs;
using Cms.Application.Content.Interfaces;
using Cms.Application.Content.Queries.GetContentItem;
using MediatR;

public class GetContentItemQueryHandler : IRequestHandler<GetContentItemQuery, ContentItemDto?>
{
    private readonly IContentQueryRepository _contentQueryRepository;

    public GetContentItemQueryHandler(IContentQueryRepository contentQueryRepository)
    {
        _contentQueryRepository = contentQueryRepository;
    }

    public async Task<ContentItemDto?> Handle(GetContentItemQuery request, CancellationToken cancellationToken)
    {
        if (request.Id is not null)
            return await _contentQueryRepository.GetByIdAsync(request.ContentType, request.Id.Value);

        if (!string.IsNullOrWhiteSpace(request.Slug))
            return await _contentQueryRepository.GetBySlugAsync(request.ContentType, request.Slug);

        throw new ArgumentException("Either Id or Slug must be provided.");
    }
}