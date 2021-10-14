using System;
using System.Collections.Generic;

namespace Application.Features.RecipeFeatures.Queries
{
    public class RecipeModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public string Instructions { get; set; } = "";
        public List<string> Ingredients { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public byte[] Image { get; set; } = Array.Empty<byte>();
    }
}