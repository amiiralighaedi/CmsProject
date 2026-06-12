

namespace Cms.Application.Common.Events;

public record ContentUpdateEvent(
    Guid ContentItemId,
    string ContentType,
    string Slug
    );
