

namespace Cms.Application.Common.Events;

public record ContentPublishedEvent(
    Guid ContentItemId,
    string ContentType,
    string Slug
    );
