using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Repositories;
using Application.Specification.RecipeSpecifications;
using Domain;
using MediatR;

namespace Application.Features.RecipeFeatures.Queries
{
    public class GetAllRecipesQuery : IRequest<IEnumerable<Recipe>>
    {
        private int _itemsOnPage = 10;
        private int _page = 1;

        public int Page
        {
            get => _page;
            set => _page = value > 0 ? value : 1;
        }

        public int ItemsOnPage
        {
            get => _itemsOnPage;
            set => _itemsOnPage = value > 0 ? value : 10;
        }

        public string SearchString { get; set; } = "";
        
        public class Handler : IRequestHandler<GetAllRecipesQuery, IEnumerable<Recipe>>
        {
            private readonly IBaseRepository<Recipe> _repository;

            public Handler(IBaseRepository<Recipe> repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<Recipe>> Handle(
                GetAllRecipesQuery request, 
                CancellationToken cancellationToken = default)
            {
                var total = await _repository.Count(
                    new FilteredByStringRecipes(request.SearchString),
                    cancellationToken);

                var skip = (request.Page - 1) * request.ItemsOnPage;
                
                var recipes = await _repository.List(
                    new PagedOrderedRecipes(skip, request.ItemsOnPage),
                    cancellationToken);

                return recipes;
            }
        }
    }
}