

using Cms.Domain.Media.MediaItems;

namespace Cms.Application.Media.Interfaces;

public interface IMediaReadRepository
{
    Task<List<MediaItem>> ListAsync();
}
