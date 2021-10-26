using Domain;

namespace Application.Specification.RecipeSpecifications
{
    public class FilteredByStringRecipes : Specification<Recipe>
    {
        public FilteredByStringRecipes(string searchString)
            : base(string.IsNullOrWhiteSpace(searchString) 
                ? x => true
                : x => x.Ingredients.Contains(searchString) 
                       || x.Title.Contains(searchString))
        {
            
        }
    }
}