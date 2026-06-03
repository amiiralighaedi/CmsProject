

using MediatR;
using Microsoft.AspNetCore.Http;

namespace Cms.Application.Media.Commands.UploadMedia;

public record UploadMediaCommands(IFormFile File) : IRequest<Guid>;
