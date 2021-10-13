using FluentValidation;

namespace Application.Features.RecipeFeatures.Commands.Create
{
    public class CreateRecipeCommandValidator : AbstractValidator<CreateRecipeCommand>
    {
        public CreateRecipeCommandValidator()
        {
            RuleFor(x => x.Title).Length(4, 200).WithMessage("Test message");
            RuleFor(x => x.Instructions).NotEmpty();
            RuleFor(x => x.Ingredients).NotEmpty();
        }
    }
}