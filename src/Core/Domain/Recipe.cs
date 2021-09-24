using System;
using System.Collections.Generic;

namespace Domain
{
    public class Recipe : BaseEntity
    {
        public string Title { get; set; } = "";
        public string Instructions { get; set; } = "";

        public Guid ImageId { get; set; }
        public Image Image { get; set; } = null!;

        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}