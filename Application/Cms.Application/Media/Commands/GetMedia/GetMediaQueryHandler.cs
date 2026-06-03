

using Cms.Application.Media.Interfaces;
using Cms.Domain.Media.MediaItems;
using MediatR;

namespace Cms.Application.Media.Commands.GetMedia;

public class GetMediaQueryHandler : IRequestHandler<GetMediaQuery, MediaItem?>
{
    private readonly IMediaRepository _repo;

    public GetMediaQueryHandler(IMediaRepository repo)
    {
        _repo = repo;
    }

    public async Task<MediaItem?> Handle(GetMediaQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetByIsAsync(request.Id);
    }
}
