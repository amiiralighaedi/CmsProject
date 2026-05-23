

using FluentValidation;

namespace Cms.Application.Content.ContentTypes.Commands.CreateContentType;

public class CreateContentTypeCommandValidator : AbstractValidator<CreateContentTypeCommand>
{
    public CreateContentTypeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Slug)
            .NotEmpty()
            .MaximumLength(100);
    }
}
