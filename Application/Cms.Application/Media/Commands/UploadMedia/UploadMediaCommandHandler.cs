
using Cms.Application.Media.Interfaces;
using Cms.Application.Media.Interfacesp;
using Cms.Domain.Media.MediaItems;
using Cms.Domain.Media.MediaTypes;
using MediatR;

namespace Cms.Application.Media.Commands.UploadMedia;

public class UploadMediaCommandHandler : IRequestHandler<UploadMediaCommands, Guid>
{
    private readonly IMediaRepository _repo;
    private readonly IFileStorageService _fileStorageService;

    public UploadMediaCommandHandler(IMediaRepository repo, IFileStorageService fileStorageService)
    {
        _repo = repo;
        _fileStorageService = fileStorageService;
    }

    public async Task<Guid> Handle(UploadMediaCommands request, CancellationToken cancellationToken)
    {
        var file = request.File;

        var url = await _fileStorageService.SaveFileAsync(file);

        var media = MediaItem.Create(
            file.FileName,
            url,
            file.Length,
            file.ContentType,
            DetectType(file.ContentType)
            );

        await _repo.AddAsync(media);
        await _repo.SaveChangesAsync();

        return media.Id;
    }

    private MediaType DetectType(string mime)
    {
        if (mime.StartsWith("image")) return MediaType.Image;
        if (mime.StartsWith("video")) return MediaType.Videos;
        if (mime.StartsWith("audio")) return MediaType.Audio;
        if (mime.Contains("pdf")) return MediaType.Document;

        return MediaType.Other;
    }
}
