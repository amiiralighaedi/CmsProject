

namespace Cms.Application.Common.Events;

public record ContentDeleteEvent(
    Guid ContentItemId,
    string ContentType,
    string Slug
    );
