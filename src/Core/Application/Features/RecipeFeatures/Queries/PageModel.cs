using System.Collections.Generic;

namespace Application.Features.RecipeFeatures.Queries
{
    public class PageModel<T> where T : class
    {
        public int Page { get; set; }
        public int ItemsOnPage { get; set; }
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; } = new List<T>();
    }
}