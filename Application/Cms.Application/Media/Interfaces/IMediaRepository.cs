

using Cms.Domain.Media.MediaItems;

namespace Cms.Application.Media.Interfaces;

public interface IMediaRepository
{
    Task<MediaItem?> GetByIsAsync(Guid id);
    Task AddAsync(MediaItem mediaItem);
    Task DeleteAsync(MediaItem mediaItem);
    Task SaveChangesAsync();
}
