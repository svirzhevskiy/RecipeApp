using FluentValidation;

namespace Models.Recipe
{
    public class RecipeValidator : AbstractValidator<RecipeModel>
    {
        public RecipeValidator()
        {
            RuleFor(x => x.Title).Length(4, 200);
            RuleFor(x => x.Instructions).NotEmpty();
            RuleFor(x => x.Ingredients).NotEmpty();
        }
    }
}