using AutoMapper;

namespace Models.Recipe
{
    public class RecipeMapping : Profile
    {
        public RecipeMapping()
        {
            CreateMap<Domain.Recipe, RecipeModel>()
                .ReverseMap();
        }
    }
}