
using Cms.Application.Media.Interfacesp;
using Microsoft.AspNetCore.Http;

namespace Cms.Infrastructure.Media.Services;

public class FileStorageService : IFileStorageService
{
    private readonly string _rootPath;

    public FileStorageService()
    {
        _rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "media");

        if (!Directory.Exists(_rootPath))
            Directory.CreateDirectory(_rootPath);
        
    }

    public void DeleteFile(string url)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", url.TrimStart('/'));

        if (File.Exists(filePath))
            File.Delete(filePath);
    }

    public async Task<string> SaveFileAsync(IFormFile file)
    {
        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(_rootPath, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"/uploads/{fileName}";
    }
}