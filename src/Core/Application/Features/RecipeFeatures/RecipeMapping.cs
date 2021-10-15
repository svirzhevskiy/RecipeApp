using Application.Features.RecipeFeatures.Commands.Create;
using Application.Features.RecipeFeatures.Queries;
using AutoMapper;
using Domain;

namespace Application.Features.RecipeFeatures
{
    public class RecipeMapping : Profile
    {
        public RecipeMapping()
        {
            CreateMap<CreateRecipeCommand, Recipe>();

            CreateMap<Recipe, RecipeModel>()
                .ForMember(x => x.Image, y => y.MapFrom(s => s.Image.Content));
        }
    }
}