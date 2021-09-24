using System.Collections.Generic;

namespace Domain
{
    public class Ingredient : BaseEntity
    {
        public string Title { get; set; } = "";

        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}