using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Common;
using Application.Repositories;
using Application.Specification.RecipeSpecifications;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.RecipeFeatures.Queries
{
    public class GetAllRecipesQuery : IRequest<PageModel<RecipeModel>>, ICacheableMediatrQuery
    {
        public bool BypassCache => !string.IsNullOrWhiteSpace(SearchString);
        public string CacheKey => $"Recipes-{Page}-{ItemsOnPage}";
        public TimeSpan? SlidingExpiration { get; set; }
        
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

        public class Handler : IRequestHandler<GetAllRecipesQuery, PageModel<RecipeModel>>
        {
            private readonly IBaseRepository<Recipe> _repository;
            private readonly IMapper _mapper;

            public Handler(IBaseRepository<Recipe> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<PageModel<RecipeModel>> Handle(
                GetAllRecipesQuery request,
                CancellationToken cancellationToken = default)
            {
                var total = await _repository.Count(
                    new FilteredByStringRecipes(request.SearchString),
                    cancellationToken);

                var skip = (request.Page - 1) * request.ItemsOnPage;

                var recipes = await _repository.List(
                    new PagedOrderedRecipes(skip, request.ItemsOnPage, request.SearchString),
                    cancellationToken);

                return new PageModel<RecipeModel>
                {
                    Page = request.Page,
                    ItemsOnPage = request.ItemsOnPage,
                    TotalItems = total,
                    Items = recipes.Select(x => _mapper.Map<RecipeModel>(x))
                };
            }
        }
    }
}