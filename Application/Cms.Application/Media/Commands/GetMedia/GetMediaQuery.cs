

using Cms.Domain.Media.MediaItems;
using MediatR;

namespace Cms.Application.Media.Commands.GetMedia;

public record GetMediaQuery(Guid Id) : IRequest<MediaItem?>;