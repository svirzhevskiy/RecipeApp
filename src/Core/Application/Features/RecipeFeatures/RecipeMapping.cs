using Application.Features.RecipeFeatures.Commands.Create;
using AutoMapper;
using Domain;

namespace Application.Features.RecipeFeatures
{
    public class RecipeMapping : Profile
    {
        public RecipeMapping()
        {
            CreateMap<CreateRecipeCommand, Recipe>()
                .ReverseMap();
        }
    }
}