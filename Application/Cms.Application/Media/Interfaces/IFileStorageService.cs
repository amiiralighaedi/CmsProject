

using Microsoft.AspNetCore.Http;

namespace Cms.Application.Media.Interfacesp;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(IFormFile file);
    void DeleteFile(string url);
}