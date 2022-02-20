using FluentValidation;

namespace ShopService.Book.Application.Command.Validation
{
    public class CreateBookCommandValidator: AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(p => p.name).NotEmpty()
                .WithMessage("{PropertyName} Empty").MaximumLength(50);

            RuleFor(p => p.publishDate).NotEmpty();

            RuleFor(p => p.AuthorId).NotEmpty();
        }
    }
}
