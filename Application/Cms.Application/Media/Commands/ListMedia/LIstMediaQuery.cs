

using Cms.Application.Media.Interfaces;
using Cms.Domain.Media.MediaItems;
using MediatR;

namespace Cms.Application.Media.Commands.ListMedia;

public record LIstMediaQuery() : IRequest<List<MediaItem>>;


public class LIstMediaQueryHandler : IRequestHandler<LIstMediaQuery, List<MediaItem>>
{
    private readonly IMediaReadRepository _mediaReadRepository;

    public LIstMediaQueryHandler(IMediaReadRepository mediaReadRepository)
    {
        _mediaReadRepository = mediaReadRepository;
    }

    public async Task<List<MediaItem>> Handle(LIstMediaQuery request, CancellationToken cancellationToken)
    {
        return await _mediaReadRepository.ListAsync();
    }
}