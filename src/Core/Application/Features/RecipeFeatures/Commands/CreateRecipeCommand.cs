using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;
using Models.Recipe;

namespace Application.Features.RecipeFeatures.Commands
{
    public class CreateRecipeCommand : IRequest<RecipeModel>
    {
        public RecipeModel Recipe { get; set; } = null!;

        public class Handler : IRequestHandler<CreateRecipeCommand, RecipeModel>
        {
            private readonly IBaseRepository<Recipe> _repository;
            private readonly IMapper _mapper;

            public Handler(IBaseRepository<Recipe> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<RecipeModel> Handle(
                CreateRecipeCommand request,
                CancellationToken cancellationToken)
            {
                if (request.Recipe is null)
                {
                    throw AppException.InvalidModel;
                }
                
                var entity = _mapper.Map<Recipe>(request.Recipe);

                var recipe = await _repository.Create(entity, cancellationToken);

                return _mapper.Map<RecipeModel>(recipe);
            }
        }
    }
}