using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Models.Recipe
{
    public class RecipeModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public string Instructions { get; set; } = "";
        public List<string> Ingredients { get; set; } = new();
        public DateTime CreatedAt { get; set; }

        public IFormFile File { get; set; } = null!;
        public byte[] Image { get; set; } = null!;
    }
}