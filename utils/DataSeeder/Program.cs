using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Database;
using DataSeeder;
using Domain;
using Microsoft.EntityFrameworkCore;

var connectionString = "Host=localhost;Port=5432;Database=RecipeMonster;Username=postgres;Password=postgres;";
var context = new AppDbContext(
    new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(connectionString).Options);

WriteImages();

using var reader = new StreamReader("./../../../data.csv");

var line = reader.ReadLine(); //skip headers
var count = 0;
var total = 13500;

while ((line = reader.ReadLine()) != null)
{
    while (line.Count(c => c == '"') % 2 != 0)
    {
        line += reader.ReadLine();
    }
    
    var data = line.GetData(',');

    if (data.Count != 6)
    {
        continue;
    }

    if (WriteRecipe(data))
    {
        count++;
        Console.WriteLine($"Recipe {data[0]} successfully added. {(double)count / total * 100:000.00}%");
    }
}

context.SaveChanges();

Console.WriteLine($"Total added recipes: {count}. {(double)count / total * 100:000.00}%");

bool WriteRecipe(List<string> data)
{
    var date = DateTime.Now;
    try
    {
        var recipe = new Recipe
        {
            Title = data[1],
            Instructions = data[3],
            CreatedAt = date
        };

        if (!string.IsNullOrWhiteSpace(data[4]))
        {
            recipe.ImageId = context.Images.First(x => x.Name == data[4]).Id;
        }

        context.Recipes.Add(recipe);
    }
    catch
    {
        return false;
    }
    
    return true;
}

void WriteImages()
{
    var dir = Directory.GetFiles("./../../../Files/Food Images");

    var i = 0;

    foreach (var imagePath in dir)
    {
        var imageInfo = new FileInfo(imagePath);

        var imageData = new byte[imageInfo.Length];

        using var imageStream = imageInfo.OpenRead();
        imageStream.Read(imageData, 0, imageData.Length);

        var image = new Image
        {
            Name = imageInfo.Name[..imageInfo.Name.LastIndexOf('.')],
            Extension = imageInfo.Extension,
            Content = imageData
        };

        context.Images.Add(image);
        i++;

        Console.WriteLine($"Image successfully added. {(double)i / 13582 * 100:000.00}%");
    }

    context.SaveChanges();
}