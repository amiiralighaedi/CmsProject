

using Cms.Application.Media.Interfaces;
using Cms.Application.Media.Interfacesp;
using MediatR;

namespace Cms.Application.Media.Commands.DeleteMedia;

public class DeleteMediaCommandHandler : IRequestHandler<DeleteMediaCommand, Unit>
{
    private readonly IMediaRepository _repo;
    private readonly IFileStorageService _storage;

    public DeleteMediaCommandHandler(IMediaRepository repo, IFileStorageService storage)
    {
        _repo = repo;
        _storage = storage;
    }

    public async Task<Unit> Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
    {
        var media = await _repo.GetByIsAsync(request.Id);

        if (media == null)
            throw new Exception("Media not found");

        // حذف فایل از FileSystem
        _storage.DeleteFile(media.Url);

        // حذف رکورد از دیتابیس
        await _repo.DeleteAsync(media);
        await _repo.SaveChangesAsync();

        return Unit.Value;
    }
}
