

using MediatR;

namespace Cms.Application.Media.Commands.DeleteMedia;

public record DeleteMediaCommand(Guid Id) : IRequest<Unit>;