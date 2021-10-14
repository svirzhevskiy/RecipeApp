using Domain;
//#define WithImages

namespace Application.Specification.RecipeSpecifications
{
    public sealed class PagedOrderedRecipes : Specification<Recipe>
    {
        public PagedOrderedRecipes(int skip, int take, bool descendingOrder = true)
        {
#if WithImages
            AddInclude(x => x.Image);
#endif

            if (descendingOrder)
            {
                ApplyOrderByDescending(x => x.CreatedAt);
            }
            else
            {
                ApplyOrderByDescending(x => x.CreatedAt);
            }
            
            ApplyPaging(skip, take);
        }
        
        public PagedOrderedRecipes(int skip, int take, string searchString, bool descendingOrder = true)
        : base(x => x.Ingredients.Contains(searchString) || x.Title.Contains(searchString))
        {
#if WithImages
            AddInclude(x => x.Image);
#endif

            if (descendingOrder)
            {
                ApplyOrderByDescending(x => x.CreatedAt);
            }
            else
            {
                ApplyOrderByDescending(x => x.CreatedAt);
            }
            
            ApplyPaging(skip, take);
        }
    }
}