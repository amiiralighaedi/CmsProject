

using Cms.Domain.Media.MediaTypes;
using Cms.Shared.Domain.Entities;

namespace Cms.Domain.Media.MediaItems;

public class MediaItem : BaseEntity, IAggregateRoot
{
    public Guid Id { get; private set; }
    public string FileName { get; private set; }
    public string Url { get; private set; }
    public long Size { get; private set; }
    public string MimeType { get; private set; }
    public MediaType Type { get; private set; }

    private MediaItem()
    {
        
    }

    public MediaItem(string fileName, string url, long size, string mimeType, MediaType type)
    {
        Id = Guid.NewGuid();
        FileName = fileName;
        Url = url;
        Size = size;
        MimeType = mimeType;
        Type = type;
        CreatedAt = DateTime.UtcNow;
    }

    public static MediaItem Create(string fileName, string url, long size, string mimeType, MediaType type)
           => new(fileName, url, size, mimeType, type);

}
